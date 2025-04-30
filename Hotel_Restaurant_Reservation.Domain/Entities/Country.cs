namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class Country 
{
    // Key Properties
    public Guid Id { get; set; }

    public string Name { get; set; }

    // Foreign Keys

    // Navigation Properties

    public virtual ICollection<City> Cities { get; set; }

    public virtual ICollection<Location> Locations { get; set; }

    public Country()
    {
        Cities = new HashSet<City>();
    }
}
