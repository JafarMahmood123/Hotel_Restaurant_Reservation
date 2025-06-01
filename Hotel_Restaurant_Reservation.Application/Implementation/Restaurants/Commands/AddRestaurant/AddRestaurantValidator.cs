using FluentValidation;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;

public class AddRestaurantValidator : AbstractValidator<AddRestaurantRequest>
{
    public AddRestaurantValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must be less than 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must be less than 500 characters");

        RuleFor(x => x.Url)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.Url))
            .WithMessage("URL must be valid");

        RuleFor(x => x.PictureUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.PictureUrl))
            .WithMessage("Picture URL must be valid");

        RuleFor(x => x.NumberOfTables)
            .GreaterThan(0).WithMessage("Number of tables must be positive");
    }
}