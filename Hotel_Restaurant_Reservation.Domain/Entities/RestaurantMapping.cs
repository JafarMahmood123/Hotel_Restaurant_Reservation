using System.ComponentModel.DataAnnotations;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Domain.Mappings;

public class RestaurantMapping
{
    public Guid Id { get; set; }

    public Guid RestaurantId { get; set; }

    public string YelpBusinessId { get; set; }

    public virtual Restaurant Restaurant { get; set; }
}