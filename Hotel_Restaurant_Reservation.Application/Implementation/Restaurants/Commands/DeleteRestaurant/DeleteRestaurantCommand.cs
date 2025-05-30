using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand : ICommand<Result<RestaurantResponse>>
{
    public DeleteRestaurantCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}