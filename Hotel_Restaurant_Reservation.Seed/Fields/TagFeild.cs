using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Seed.Fields;

internal class TagFeild
{

    public string Name { get; set; }

    public TagFeild(string name)
    {
        Name = name;
    }
}
