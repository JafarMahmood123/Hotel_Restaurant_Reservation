using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand : ICommand<Result<RestaurantResponse>>
{
    public UpdateRestaurantCommand(Guid id, UpdateRestaurantRequest restaurantUpdateRequest)
    {
        Id = id;
        RestaurantUpdateRequest = restaurantUpdateRequest;
    }

    public Guid Id { get; }
    public UpdateRestaurantRequest RestaurantUpdateRequest { get; }
}