namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Event
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartingDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public double PayToEnter { get; set; }

    public int MaxNumberOfRegesters { get; set; }
}
