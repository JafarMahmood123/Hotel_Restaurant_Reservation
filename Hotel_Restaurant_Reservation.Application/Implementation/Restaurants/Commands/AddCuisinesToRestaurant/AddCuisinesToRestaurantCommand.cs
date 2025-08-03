using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisinesToRestaurantCommand : ICommand<Result<CuisineResponse>>
{
    public AddCuisinesToRestaurantCommand(Guid restaurantId, Guid cuisineId)
    {
        RestaurantId = restaurantId;
        CuisineId = cuisineId;
    }

    public Guid RestaurantId { get; }
    public Guid CuisineId { get; }
}
