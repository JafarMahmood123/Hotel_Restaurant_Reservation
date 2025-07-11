using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.DeletePropertyType;

public class DeletePropertyTypeCommand : ICommand<Result>
{
    public DeletePropertyTypeCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
