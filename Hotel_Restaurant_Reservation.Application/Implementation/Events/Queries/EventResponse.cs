namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Queries;

public class EventResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartingDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double PayToEnter { get; set; }
    public int MaxNumberOfRegesters { get; set; }
    public Guid LocationId { get; set; }
    public Guid CurrencyTypeId { get; set; }
}