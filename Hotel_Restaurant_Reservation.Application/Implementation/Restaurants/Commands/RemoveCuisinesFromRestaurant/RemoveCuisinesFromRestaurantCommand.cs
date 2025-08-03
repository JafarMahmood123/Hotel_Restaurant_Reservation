using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisinesFromRestaurant;

public class RemoveCuisinesFromRestaurantCommand : ICommand<Result<CuisineResponse>>
{
    public RemoveCuisinesFromRestaurantCommand(Guid restaurantId, Guid cuisineId)
    {
        RestaurantId = restaurantId;
        CuisineId = cuisineId;
    }

    public Guid RestaurantId { get; }
    public Guid CuisineId { get; }
}