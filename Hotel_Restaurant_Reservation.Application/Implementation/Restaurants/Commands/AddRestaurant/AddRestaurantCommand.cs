using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.RestaurantDTOs;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommand : ICommand<Restaurant>
{
    public Restaurant Restaurant { get; set; }  

    public Location Location { get; set; }


    public AddRestaurantCommand(Restaurant restaurant, Location location)
    {
        Restaurant = restaurant;
        Location = location;
    }
}
