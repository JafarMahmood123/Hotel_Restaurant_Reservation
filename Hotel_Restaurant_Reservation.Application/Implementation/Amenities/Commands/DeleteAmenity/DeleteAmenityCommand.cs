using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Commands.DeleteAmenity;

public class DeleteAmenityCommand : ICommand<Result>
{
    public DeleteAmenityCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}