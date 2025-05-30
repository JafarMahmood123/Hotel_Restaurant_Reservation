using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveWorkTimesFromRestaurant;

public class RemoveWorkTimesFromRestaurantCommand : ICommand<Result<List<WorkTimeResponse>>>
{
    public RemoveWorkTimesFromRestaurantCommand(
        Guid restaurantId,
        RemoveWorkTimesFromRestaurantRequest removeWorkTimesFromRestaurantRequest)
    {
        RestaurantId = restaurantId;
        RemoveWorkTimesFromRestaurantRequest = removeWorkTimesFromRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public RemoveWorkTimesFromRestaurantRequest RemoveWorkTimesFromRestaurantRequest { get; }
}