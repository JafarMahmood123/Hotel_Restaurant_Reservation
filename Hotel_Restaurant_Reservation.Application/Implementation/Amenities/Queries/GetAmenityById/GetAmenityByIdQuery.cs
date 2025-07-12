using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries.GetAmenityById;

public class GetAmenityByIdQuery : IQuery<Result<AmenityResponse>>
{
    public GetAmenityByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}