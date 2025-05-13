using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand : ICommand<Restaurant?>
{
    public DeleteRestaurantCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
