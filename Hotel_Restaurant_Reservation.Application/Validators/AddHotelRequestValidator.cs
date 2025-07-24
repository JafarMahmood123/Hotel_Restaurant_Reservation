using FluentValidation;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;

public class AddHotelRequestValidator : AbstractValidator<AddHotelRequest>
{
    public AddHotelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("URL is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("A valid URL is required.");

        RuleFor(x => x.StarRate)
            .InclusiveBetween(1, 5).WithMessage("Star rate must be between 1 and 5.");

        RuleFor(x => x.NumberOfRooms)
            .GreaterThan(0).WithMessage("Number of rooms must be greater than 0.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

        RuleFor(x => x.LocationId)
            .NotEmpty().WithMessage("Location ID is required.");
    }
}