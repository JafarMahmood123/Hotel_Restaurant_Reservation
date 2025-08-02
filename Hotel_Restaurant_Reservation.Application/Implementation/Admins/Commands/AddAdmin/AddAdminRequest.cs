namespace Hotel_Restaurant_Reservation.Application.Implementation.Admins.Commands.AddAdmin;

public class AddAdminRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateOnly BirthDate { get; set; }

    public Guid LocationId { get; set; }
}
