using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries.GetPropertyTypeById;

public class GetPropertyTypeByIdQuery : IQuery<Result<PropertyTypeResponse>>
{
    public GetPropertyTypeByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}