namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Queries;

public class CustomerResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string HashedPassword { get; set; }

    public DateOnly BirthDate { get; set; }

    public int Age { get; set; }
}
