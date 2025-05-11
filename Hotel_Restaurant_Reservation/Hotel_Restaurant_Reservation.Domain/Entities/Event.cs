namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Event
{
    // Key Properties

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartingDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public double PayToEnter { get; set; }

    public int MaxNumberOfRegesters { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }

    public Guid CurrencyTypeId { get; set; }

    // Navigation Properties

    public virtual ICollection<EventRegistration> EventRegistrations { get; set; }

    public virtual Location Location {  get; set; }

    public virtual ICollection<CurrencyType> CurrencyTypes { get; set; }

    public Event()
    {
        EventRegistrations = new HashSet<EventRegistration>();

        CurrencyTypes = new HashSet<CurrencyType>();
    }
}
