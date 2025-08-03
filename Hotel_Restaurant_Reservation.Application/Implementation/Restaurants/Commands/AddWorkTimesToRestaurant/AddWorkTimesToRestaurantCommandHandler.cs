using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddWorkTimesToRestaurant;

public class AddWorkTimesToRestaurantCommandHandler
    : ICommandHandler<AddWorkTimesToRestaurantCommand, Result<List<WorkTimeResponse>>>
{
    // You need a repository for Restaurants to perform the validation
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IGenericRepository<WorkTime> _workTimeRepository;
    private readonly IGenericRepository<RestaurantWorkTime> _restaurantWorkTimeRepository;
    private readonly IMapper _mapper;

    public AddWorkTimesToRestaurantCommandHandler(
        IGenericRepository<Restaurant> restaurantRepository, // Add this to your constructor
        IGenericRepository<WorkTime> workTimeRepository,
        IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository; // And assign it here
        _workTimeRepository = workTimeRepository;
        _restaurantWorkTimeRepository = restaurantWorkTimeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<WorkTimeResponse>>> Handle(AddWorkTimesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var workTimeId = request.WorkTimeId;

        // =================================================================
        // BUG FIX: VALIDATE YOUR INPUTS FIRST
        // =================================================================

        // 1. Check if the Restaurant exists
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
        {
            // Fail early with a clear error instead of crashing the database
            return Result.Failure<List<WorkTimeResponse>>(
                DomainErrors.Restaurant.NotFound(restaurantId));
        }

        // 2. Check if the WorkTime exists
        var workTime = await _workTimeRepository.GetByIdAsync(workTimeId);
        if (workTime == null)
        {
            return Result.Failure<List<WorkTimeResponse>>(
                DomainErrors.WorkTime.NotFound(workTimeId));
        }

        // --- Your existing logic continues here ---

        var restaurantWorkTimes = await _restaurantWorkTimeRepository.Where(
            x => x.RestaurantId == restaurantId)
            .Include(x => x.WorkTime)
            .Select(x => x.WorkTime)
            .ToListAsync(cancellationToken);

        // ... rest of your complex merging logic ...

        // NOTE: The rest of your merging logic is highly complex and may contain other bugs.
        // It is recommended to simplify this logic significantly. However, the validation
        // added above will fix the specific crash you are currently experiencing.

        restaurantWorkTimes.Add(workTime);
        var mergedWorkTimes = new List<WorkTime>();
        var workTimesToDelete = new List<WorkTime>();

        // Group by day and process each day separately
        foreach (var dayGroup in restaurantWorkTimes.GroupBy(x => x.Day))
        {
            var dayWorkTimes = dayGroup.OrderBy(x => x.OpenHour).ToList();
            if (!dayWorkTimes.Any()) continue;

            var currentMerged = dayWorkTimes.First();

            for (int i = 1; i < dayWorkTimes.Count; i++)
            {
                if (dayWorkTimes[i].OpenHour <= currentMerged.CloseHour)
                {
                    workTimesToDelete.Add(dayWorkTimes[i]);
                    if (dayWorkTimes[i].CloseHour > currentMerged.CloseHour)
                    {
                        currentMerged.CloseHour = dayWorkTimes[i].CloseHour;
                    }
                }
                else
                {
                    mergedWorkTimes.Add(currentMerged);
                    currentMerged = dayWorkTimes[i];
                }
            }
            mergedWorkTimes.Add(currentMerged);
        }

        var associationsToDelete = await _restaurantWorkTimeRepository
            .Where(x => x.RestaurantId == restaurantId)
            .ToListAsync(cancellationToken);

        _restaurantWorkTimeRepository.RemoveRange(associationsToDelete);

        var newAssociations = new List<RestaurantWorkTime>();
        foreach (var merged in mergedWorkTimes)
        {
            newAssociations.Add(new RestaurantWorkTime
            {
                Id = Guid.NewGuid(),
                RestaurantId = restaurantId,
                WorkTimeId = merged.Id // This assumes merged work times are existing entities
                                       // This part of the logic is complex and may need review
            });
        }

        await _restaurantWorkTimeRepository.AddRangeAsync(newAssociations);
        await _restaurantWorkTimeRepository.SaveChangesAsync();

        var response = _mapper.Map<List<WorkTimeResponse>>(mergedWorkTimes);
        return Result.Success(response);
    }
}
