namespace Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.UpdateCustomer
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public Guid LocationId { get; set; }
    }
}