using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisinesFromRestaurant;

public class RemoveCuisinesFromRestaurantCommand : ICommand<Result<List<CuisineResponse>>>
{
    public RemoveCuisinesFromRestaurantCommand(
        Guid restaurantId,
        RemoveCuisineFromRestaurantRequest removeCuisineFromRestaurantRequest)
    {
        RestaurantId = restaurantId;
        RemoveCuisineFromRestaurantRequest = removeCuisineFromRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public RemoveCuisineFromRestaurantRequest RemoveCuisineFromRestaurantRequest { get; }
}