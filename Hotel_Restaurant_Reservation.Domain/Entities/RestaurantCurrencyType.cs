namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantCurrencyType
{
    // Key Properties
    public Guid Id { get; set; }

    // Foreign Keys

    public Guid RestaurantId { get; set; }

    public Guid CurrencyTypeId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual CurrencyType CurrencyType { get; set; }

    public RestaurantCurrencyType()
    {

    }
}
