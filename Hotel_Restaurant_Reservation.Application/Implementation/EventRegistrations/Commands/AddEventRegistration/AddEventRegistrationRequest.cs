namespace Hotel_Restaurant_Reservation.Application.Implementation.EventRegistrations.Commands.AddEventRegistration;

public class AddEventRegistrationRequest
{
    public Guid CustomerId { get; set; }
    public Guid EventId { get; set; }
    public int NumberOfPeople { get; set; }
}