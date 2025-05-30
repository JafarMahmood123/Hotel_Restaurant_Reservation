using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.CuisineDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisinesToRestaurantCommand : ICommand<Result<List<CuisineResponse>>>
{
    public AddCuisinesToRestaurantCommand(Guid restaurantId, AddCuisineToRestaurantRequest addCuisineToRestaurantRequest)
    {
        RestaurantId = restaurantId;
        AddCuisineToRestaurantRequest = addCuisineToRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public AddCuisineToRestaurantRequest AddCuisineToRestaurantRequest { get; }
}
