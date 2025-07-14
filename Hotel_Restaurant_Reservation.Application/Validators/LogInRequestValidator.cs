using FluentValidation;
using Hotel_Restaurant_Reservation.Application.Implementation.Users.Commands.LogIn;

namespace Hotel_Restaurant_Reservation.Presentation.Validators;

public class LogInRequestValidator : AbstractValidator<LogInRequest>
{
    public LogInRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}
