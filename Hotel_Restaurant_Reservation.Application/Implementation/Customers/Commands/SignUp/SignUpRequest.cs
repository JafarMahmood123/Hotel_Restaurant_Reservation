namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;

public class SignUpRequest
{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateOnly BirthDate { get; set; }

    // Foreign Keys

    public Guid LocationId { get; set; }
}
