using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.CheckExistingLocationWithoutLocalLocation;

public class CheckExistingLocationWithoutLocalLocationCommand : ICommand<Result<Guid>>
{
    public CheckExistingLocationWithoutLocalLocationCommand(CheckExistingLocationWithoutLocalLocationRequest checkExistingLocationRequest)
    {
        CheckExistingLocationRequest = checkExistingLocationRequest;
    }

    public CheckExistingLocationWithoutLocalLocationRequest CheckExistingLocationRequest { get; }
}
