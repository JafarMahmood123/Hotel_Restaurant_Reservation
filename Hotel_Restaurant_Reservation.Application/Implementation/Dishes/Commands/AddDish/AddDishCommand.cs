using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.AddDish;

public class AddDishCommand : ICommand<Result<DishResponse>>
{

    public AddDishCommand(AddDishRequest addDishRequest)
    {
        AddDishRequest = addDishRequest;
    }

    public AddDishRequest AddDishRequest { get; }
}
