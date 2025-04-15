namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class EventRegistration
{
    public Guid Id { get; set; }

    public DateTime RegistrationDateTime { get; set; }

    public int NumberOfPeople { get; set; }


}
