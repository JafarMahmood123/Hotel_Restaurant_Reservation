using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.AddDish;

public class AddDishCommand : ICommand<Dish>
{
    public Dish Dish { get; set; }

    public AddDishCommand(Dish dish)
    {
        Dish = dish;
    }
}
