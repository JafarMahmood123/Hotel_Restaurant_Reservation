using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;

public class AddLocationCommand : ICommand<Result<LocationResponse>>
{
    public AddLocationCommand(AddLocationRequest addLocationRequest)
    {
        AddLocationRequest = addLocationRequest;
    }

    public AddLocationRequest AddLocationRequest { get; }
}