namespace Hotel_Restaurant_Reservation.Application.Implementation.Events.Commands.UpdateEvent;

public class UpdateEventRequest
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