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
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IGenericRepository<RestaurantWorkTime> _restaurantWorkTimeRepository;
    private readonly IMapper _mapper;

    public AddWorkTimesToRestaurantCommandHandler(
        IGenericRepository<Restaurant> restaurantRepository,
        IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _restaurantWorkTimeRepository = restaurantWorkTimeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<WorkTimeResponse>>> Handle(AddWorkTimesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        // --- 1. Validation ---
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
        {
            return Result.Failure<List<WorkTimeResponse>>(
                DomainErrors.Restaurant.NotFound(restaurantId));
        }

        // --- 2. Prepare Data for Merging ---
        var timeSlotToAdd = _mapper.Map<RestaurantWorkTime>(request.AddWorkTimesToRestaurantRequest);
        timeSlotToAdd.RestaurantId = restaurantId;

        // --- 3. Isolate the Operation to a Single Day ---

        // Fetch only the work times for the specific day being modified.
        var sameDayWorkTimes = await _restaurantWorkTimeRepository
            .Where(wt => wt.RestaurantId == restaurantId && wt.Day == timeSlotToAdd.Day)
            .ToListAsync(cancellationToken);

        // This list will be used for the merge logic.
        var listToMerge = new List<RestaurantWorkTime>(sameDayWorkTimes) { timeSlotToAdd };

        // --- 4. Merge Logic (for the target day only) ---
        var mergedForDay = new List<RestaurantWorkTime>();
        var dayWorkTimes = listToMerge.OrderBy(x => x.OpenHour).ToList();

        if (dayWorkTimes.Any())
        {
            var currentOpen = dayWorkTimes.First().OpenHour;
            var currentClose = dayWorkTimes.First().CloseHour;

            for (int i = 1; i < dayWorkTimes.Count; i++)
            {
                var nextWorkTime = dayWorkTimes[i];
                if (nextWorkTime.OpenHour <= currentClose)
                {
                    if (nextWorkTime.CloseHour > currentClose)
                    {
                        currentClose = nextWorkTime.CloseHour;
                    }
                }
                else
                {
                    mergedForDay.Add(new RestaurantWorkTime
                    {
                        Id = Guid.NewGuid(),
                        Day = timeSlotToAdd.Day,
                        OpenHour = currentOpen,
                        CloseHour = currentClose,
                        RestaurantId = restaurantId
                    });
                    currentOpen = nextWorkTime.OpenHour;
                    currentClose = nextWorkTime.CloseHour;
                }
            }

            mergedForDay.Add(new RestaurantWorkTime
            {
                Id = Guid.NewGuid(),
                Day = timeSlotToAdd.Day,
                OpenHour = currentOpen,
                CloseHour = currentClose,
                RestaurantId = restaurantId
            });
        }

        // --- 5. Database Update (Targeted) ---

        // Remove only the old work time records for the specific day being updated.
        if (sameDayWorkTimes.Any())
        {
            _restaurantWorkTimeRepository.RemoveRange(sameDayWorkTimes);
        }

        // Add the new, merged work time records for that day.
        await _restaurantWorkTimeRepository.AddRangeAsync(mergedForDay);

        // Save all changes in a single transaction.
        await _restaurantWorkTimeRepository.SaveChangesAsync();

        // --- 6. Return Response ---
        // For a complete view, fetch all work times again after the update.
        var finalWorkTimes = await _restaurantWorkTimeRepository
            .Where(wt => wt.RestaurantId == restaurantId)
            .ToListAsync(cancellationToken);

        var response = _mapper.Map<List<WorkTimeResponse>>(finalWorkTimes);
        return Result.Success(response);
    }
}
