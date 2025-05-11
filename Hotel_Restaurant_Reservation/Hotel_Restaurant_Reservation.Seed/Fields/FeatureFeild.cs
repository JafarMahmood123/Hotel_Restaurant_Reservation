using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Seed.Fields;

internal class FeatureFeild
{

    public string Name { get; set; }

    public FeatureFeild(string name)
    {
        Name = name;
    }
}
