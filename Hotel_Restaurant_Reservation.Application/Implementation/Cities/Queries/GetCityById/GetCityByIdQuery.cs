using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityById;

public class GetCityByIdQuery : IQuery<Result<CityResponse>>
{
    public GetCityByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}