namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.AddEvent;

public class AddEventRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartingDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public double PayToEnter { get; set; }
    public int MaxNumberOfRegesters { get; set; }
    public Guid LocationId { get; set; }
    public Guid CurrencyTypeId { get; set; }
}