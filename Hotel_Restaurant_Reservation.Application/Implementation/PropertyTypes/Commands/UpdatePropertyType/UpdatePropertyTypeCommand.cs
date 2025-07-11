using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.UpdatePropertyType;

public class UpdatePropertyTypeCommand : ICommand<Result<PropertyType>>
{
    public UpdatePropertyTypeCommand(Guid id, UpdatePropertyTypeRequest updatePropertyTypeRequest)
    {
        Id = id;
        UpdatePropertyTypeRequest = updatePropertyTypeRequest;
    }

    public Guid Id { get; }
    public UpdatePropertyTypeRequest UpdatePropertyTypeRequest { get; }
}