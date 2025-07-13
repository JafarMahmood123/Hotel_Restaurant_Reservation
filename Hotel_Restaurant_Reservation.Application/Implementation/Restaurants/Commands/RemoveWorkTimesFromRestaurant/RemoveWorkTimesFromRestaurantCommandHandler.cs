using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;

public class RemoveWorkTimesFromRestaurantCommandHandler
    : ICommandHandler<RemoveWorkTimesFromRestaurantCommand, Result<List<WorkTimeResponse>>>
{
    private readonly IGenericRepository<RestaurantWorkTime> _restaurantWorkTimeRepository;
    private readonly IGenericRepository<WorkTime> _workTimeRepository;
    private readonly IMapper _mapper;

    public RemoveWorkTimesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository,
        IGenericRepository<WorkTime> workTimeRepository,
        IMapper mapper)
    {
        _restaurantWorkTimeRepository = restaurantWorkTimeRepository;
        _workTimeRepository = workTimeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<WorkTimeResponse>>> Handle(
        RemoveWorkTimesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var workTimeIds = request.RemoveWorkTimesFromRestaurantRequest.Ids;

        // Verify all work times exist
        var workTimes = new List<WorkTime>();
        foreach (var workTimeId in workTimeIds)
        {
            var workTime = await _workTimeRepository.GetByIdAsync(workTimeId);
            if (workTime == null)
            {
                return Result.Failure<List<WorkTimeResponse>>(
                    DomainErrors.WorkTime.NotFound(workTimeId));
            }
            workTimes.Add(workTime);
        }

        // Get all existing associations
        var restaurantWorkTimes = await _restaurantWorkTimeRepository
            .Where(x => x.RestaurantId == restaurantId && workTimeIds.Contains(x.WorkTimeId))
            .Include(x => x.WorkTime)
            .ToListAsync();

        if (!restaurantWorkTimes.Any())
        {
            return Result.Failure<List<WorkTimeResponse>>(
                DomainErrors.Restaurant.NoWorkTimesToRemove);
        }

        // Remove associations
        _restaurantWorkTimeRepository.RemoveRange(restaurantWorkTimes);
        await _restaurantWorkTimeRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<List<WorkTimeResponse>>(
            restaurantWorkTimes.Select(x => x.WorkTime).ToList());
        return Result.Success(response);
    }
}