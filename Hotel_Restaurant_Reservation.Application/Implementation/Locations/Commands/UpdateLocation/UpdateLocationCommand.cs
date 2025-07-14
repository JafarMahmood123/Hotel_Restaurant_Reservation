using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;

public class UpdateLocationCommand : ICommand<Result<LocationResponse>>
{
    public UpdateLocationCommand(Guid id, UpdateLocationRequest updateLocationRequest)
    {
        Id = id;
        UpdateLocationRequest = updateLocationRequest;
    }

    public Guid Id { get; }
    public UpdateLocationRequest UpdateLocationRequest { get; }
}