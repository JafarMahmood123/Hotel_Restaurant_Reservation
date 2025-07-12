using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Amenities.Queries.GetAllAmenities;

public class GetAllAmenitiesQuery : IQuery<Result<IEnumerable<AmenityResponse>>>
{
}