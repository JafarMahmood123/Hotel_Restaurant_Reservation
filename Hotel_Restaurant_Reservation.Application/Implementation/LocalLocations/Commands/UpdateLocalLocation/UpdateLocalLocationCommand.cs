using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.UpdateLocalLocation;

public class UpdateLocalLocationCommand : ICommand<Result<LocalLocationResponse>>
{
    public UpdateLocalLocationCommand(Guid id, UpdateLocalLocationRequest updateLocalLocationRequest)
    {
        Id = id;
        UpdateLocalLocationRequest = updateLocalLocationRequest;
    }

    public Guid Id { get; }
    public UpdateLocalLocationRequest UpdateLocalLocationRequest { get; }
}