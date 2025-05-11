using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands;

public class AddMealTypeCommand : ICommand<MealType>
{
    public MealType MealType { get; set; }

    public AddMealTypeCommand(MealType mealType)
    {
        MealType = mealType;
    }
}
