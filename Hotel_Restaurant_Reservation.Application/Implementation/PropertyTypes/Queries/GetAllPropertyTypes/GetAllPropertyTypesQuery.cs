using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries.GetAllPropertyTypes;

public class GetAllPropertyTypesQuery : IQuery<Result<IEnumerable<PropertyTypeResponse>>>
{
}