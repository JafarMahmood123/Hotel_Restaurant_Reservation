using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands;

public class AddMealTypeCommand : ICommand<Result<MealTypeResponse>>
{
    public AddMealTypeCommand(AddMealTypeRequest addMealTypeRequest)
    {
        AddMealTypeRequest = addMealTypeRequest;
    }

    public AddMealTypeRequest AddMealTypeRequest { get; }
}
