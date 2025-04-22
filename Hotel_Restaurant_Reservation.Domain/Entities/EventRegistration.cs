namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class EventRegistration
{
    // Key Properties

    public Guid Id { get; set; }

    public DateTime RegistrationDateTime { get; set; }

    public int NumberOfPeople { get; set; }

    // Foreign Keys

    public int CustomerId { get; set; }

    public int EventId { get; set; }

    // Navigation Properties

    public virtual Customer Customer { get; set; }

    public virtual Event Event { get; set; }

}
