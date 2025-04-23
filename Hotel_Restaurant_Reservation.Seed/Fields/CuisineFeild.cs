using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Seed.Fields;

internal class CuisineFeild
{

    public string Name { get; set; }

    public CuisineFeild(string name)
    {
        Name = name;
    }
}
