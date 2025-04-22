namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class RestaurantOrder
{
    // Key Properties
    public Guid Id { get; set; }

    public DateTime OrderDateTime{ get; set; }

    public DateTime ReceiveDateTime { get; set; }

    public int NumberOfPeople { get; set; }

    public int TableNumber { get; set; }

    // Foreign Keys

    public int RestaurantId { get; set; }

    public int CustomerId { get; set; }

    // Navigation Properties

    public virtual Restaurant Restaurant { get; set; }

    public virtual Customer Customers { get; set; }

    public RestaurantOrder()
    {
        
    }
}
