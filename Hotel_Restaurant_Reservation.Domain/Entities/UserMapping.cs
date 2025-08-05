using System.ComponentModel.DataAnnotations;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Domain.Mappings;

public class UserMapping
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    public string YelpUserId { get; set; }

    public virtual User User { get; set; }
}
