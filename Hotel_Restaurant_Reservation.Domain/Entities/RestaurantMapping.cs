using System.ComponentModel.DataAnnotations;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Domain.Mappings;

public class RestaurantMapping
{
    [Key]
    public Guid RestaurantId { get; set; }

    [Required]
    public string YelpBusinessId { get; set; }

    public virtual Restaurant Restaurant { get; set; }
}