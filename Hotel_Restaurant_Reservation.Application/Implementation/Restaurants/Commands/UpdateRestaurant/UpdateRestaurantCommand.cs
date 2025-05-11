using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand : ICommand<Restaurant?>
{
    public UpdateRestaurantCommand(Guid id, Restaurant restaurant)
    {
        Id = id;
        Restaurant = restaurant;
    }

    public Guid Id { get; }
    public Restaurant Restaurant { get; }
}
