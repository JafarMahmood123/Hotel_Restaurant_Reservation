using FluentValidation;
using Hotel_Restaurant_Reservation.Application.DTOs.CustomerDTOs;

namespace Hotel_Restaurant_Reservation.Presentation.Validators;

public class LogInRequestValidator : AbstractValidator<LogInRequest>
{
    public LogInRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}
