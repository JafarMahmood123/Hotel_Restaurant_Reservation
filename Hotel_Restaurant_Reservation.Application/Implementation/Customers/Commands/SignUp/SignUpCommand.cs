using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.SignUp;

public class SignUpCommand : ICommand<Customer?>
{
    public SignUpCommand(Customer customer)
    {
        Customer = customer;
    }

    public Customer Customer { get; }
}
