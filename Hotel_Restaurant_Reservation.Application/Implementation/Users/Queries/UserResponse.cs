namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Queries;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Age { get; set; }
    public string RoleName { get; set; }
    public Guid LocationId { get; set; }
}