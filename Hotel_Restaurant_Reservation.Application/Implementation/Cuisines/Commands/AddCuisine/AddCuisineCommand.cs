using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.AddCuisine;

public class AddCuisineCommand : ICommand<Cuisine>
{
    public Cuisine Cuisine { get; set; }

    public AddCuisineCommand(Cuisine cuisine)
    {
        Cuisine = cuisine;
    }
}
