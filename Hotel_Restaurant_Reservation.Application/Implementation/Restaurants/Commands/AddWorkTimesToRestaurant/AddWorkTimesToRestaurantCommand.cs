using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddWorkTimesToRestaurant;

public class AddWorkTimesToRestaurantCommand : ICommand<Result<List<WorkTimeResponse>>>
{
    public AddWorkTimesToRestaurantCommand(
        Guid restaurantId,
        AddWorkTimesToRestaurantRequest addWorkTimesToRestaurantRequest)
    {
        RestaurantId = restaurantId;
        AddWorkTimesToRestaurantRequest = addWorkTimesToRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public AddWorkTimesToRestaurantRequest AddWorkTimesToRestaurantRequest { get; }
}