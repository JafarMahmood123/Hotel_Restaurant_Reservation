using FluentValidation;
using Hotel_Restaurant_Reservation.Application.DTOs.ReviewDTOs;

namespace Hotel_Restaurant_Reservation.Presentation.Validators;

public class AddReviewRequestValidator : AbstractValidator<AddReviewRequest>
{
    public AddReviewRequestValidator()
    {
        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.CustomerStarRating).GreaterThan(0);

        RuleFor(x => x.CustomerStarRating).LessThanOrEqualTo(5);

        //ToDo..
        //Add the check for existing customer and restaurnat.
    }
}
