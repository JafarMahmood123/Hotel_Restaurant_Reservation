using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommand : ICommand<Result<RestaurantResponse>>
{
    public AddRestaurantRequest AddRestaurantRequest { get; }

    public AddRestaurantCommand(AddRestaurantRequest addRestaurantRequest)
    {
        AddRestaurantRequest = addRestaurantRequest;
    }
}