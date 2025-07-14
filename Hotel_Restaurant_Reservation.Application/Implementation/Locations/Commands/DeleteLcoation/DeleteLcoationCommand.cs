using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.DeleteLocation;

public class DeleteLocationCommand : ICommand<Result>
{
    public DeleteLocationCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}