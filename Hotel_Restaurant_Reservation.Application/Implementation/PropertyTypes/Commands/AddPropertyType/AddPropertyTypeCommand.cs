using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.AddPropertyType;

public class AddPropertyTypeCommand : ICommand<Result<PropertyTypeResponse>>
{
    public AddPropertyTypeCommand(AddPropertyTypeRequest addPropertyTypeRequest)
    {
        AddPropertyTypeRequest = addPropertyTypeRequest;
    }

    public AddPropertyTypeRequest AddPropertyTypeRequest { get; }
}