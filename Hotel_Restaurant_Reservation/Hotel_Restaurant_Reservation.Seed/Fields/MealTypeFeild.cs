using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Seed.Fields;

internal class MealTypeFeild
{
    public string Name { get; set; }

    public MealTypeFeild(string name)
    {
        Name = name;
    }
}
