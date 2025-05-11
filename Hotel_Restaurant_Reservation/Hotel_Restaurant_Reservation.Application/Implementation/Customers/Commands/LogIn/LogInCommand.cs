using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Customers.Commands.LogIn;

public class LogInCommand : ICommand<string?>
{
    public LogInCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }
    public string Password { get; }
}
