using FluentValidation;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Commands.AddReview;

namespace Hotel_Restaurant_Reservation.Presentation.Validators;

public class AddReviewRequestValidator : AbstractValidator<AddRestaurantReviewRequest>
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
