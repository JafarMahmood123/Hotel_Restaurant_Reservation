using FluentValidation;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantBookings.Commands.AddRestaurantBooking;

namespace Hotel_Restaurant_Reservation.Presentation.Validators;

public class AddRestaurantBookingRequestValidator : AbstractValidator<AddRestaurantBookingRequest>
{
    public AddRestaurantBookingRequestValidator()
    {
        RuleFor(x => x.NumberOfPeople).GreaterThan(0);

        //RuleFor(x => x.ReceiveDateTime).GreaterThan(x => x.BookingDateTime);

        //RuleFor(x => x.EndBookingDateTime).GreaterThan(x => x.ReceiveDateTime);

        // ToDo..
        /*
         Check for exisiting customer,restaurant and if the table isi free create command here
         */
    }
}
