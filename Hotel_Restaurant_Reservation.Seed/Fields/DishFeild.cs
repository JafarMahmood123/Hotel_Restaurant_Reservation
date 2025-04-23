using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Seed.Fields;

internal class DishFeild
{

    public string Name { get; set; }

    public double Price { get; set; }


    public DishFeild(string name, double price)
    {
        Name = name;
        Price = price;
    }
}
