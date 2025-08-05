using System.ComponentModel.DataAnnotations;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Domain.Mappings;

public class UserMapping
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string YelpUserId { get; set; }

    public virtual User User { get; set; }
}
