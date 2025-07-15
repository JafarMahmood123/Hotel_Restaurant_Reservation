namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class UserImage
{
    public Guid Id { get; set; }

    public string Url { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; }
}
