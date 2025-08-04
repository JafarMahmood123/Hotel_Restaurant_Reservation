using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;

public class RemoveWorkTimesFromRestaurantCommandHandler
    : ICommandHandler<RemoveWorkTimesFromRestaurantCommand, Result>
{
    private readonly IGenericRepository<RestaurantWorkTime> _restaurantWorkTimeRepository;
    private readonly IMapper _mapper;

    public RemoveWorkTimesFromRestaurantCommandHandler(IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository,
        IMapper mapper)
    {
        _restaurantWorkTimeRepository = restaurantWorkTimeRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(
        RemoveWorkTimesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var workTimeId = request.WorkTimeId;

        var workTime = await _restaurantWorkTimeRepository.GetFirstOrDefaultAsync(x => x.Id == workTimeId);

        if(workTime == null)
        {
            return Result.Failure(DomainErrors.WorkTime.NotFound(workTimeId));
        }

        await _restaurantWorkTimeRepository.RemoveAsync(workTimeId);
        await _restaurantWorkTimeRepository.SaveChangesAsync();

        return Result.Success();
    }
}