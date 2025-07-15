namespace Hotel_Restaurant_Reservation.Domain.Entities;

public class User
{
    // Key Properties
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string HashedPassword { get; set; }

    public DateOnly BirthDate { get; set; }

    public int Age { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }

    public Guid RoleId { get; set; }

    // Navigation Properties

    public virtual Location Location { get; set; }

    public virtual Role Role { get; set; }

    public virtual ICollection<UserImage> UserImages { get; set; }

    public User()
    {
    }
}
