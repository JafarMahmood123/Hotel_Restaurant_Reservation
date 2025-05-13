namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class CurrencyType
{
    // Key Properties
    public Guid Id { get; set; }

    public string CurrencyCode { get; set; }

    // Foreign Keys

    // Key Properties

    public virtual ICollection<RestaurantCurrencyType> RestaurantCurrencyTypes { get; set; }

    public CurrencyType()
    {

    }
}
