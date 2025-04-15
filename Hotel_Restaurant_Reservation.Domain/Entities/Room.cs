namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }

    public int MaxOccupancy { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }
}
