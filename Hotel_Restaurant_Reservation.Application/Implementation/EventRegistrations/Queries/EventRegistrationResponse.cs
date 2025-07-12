namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Queries;

public class EventRegistrationResponse
{
    public Guid Id { get; set; }
    public DateTime RegistrationDateTime { get; set; }
    public int NumberOfPeople { get; set; }
    public Guid CustomerId { get; set; }
    public Guid EventId { get; set; }
}