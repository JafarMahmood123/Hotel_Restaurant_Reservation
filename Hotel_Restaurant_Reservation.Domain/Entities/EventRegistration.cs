namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class EventRegistration
{
    // Key Properties

    public Guid Id { get; set; }

    public DateTime RegistrationDateTime { get; set; }

    public int NumberOfPeople { get; set; }

    // Foreign Keys

    public Guid CustomerId { get; set; }

    public Guid EventId { get; set; }

    // Navigation Properties

    public Customer Customer { get; set; }

    public Event Event { get; set; }

}
