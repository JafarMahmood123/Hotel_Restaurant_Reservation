using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryById;

public class GetCountryByIdQuery : IQuery<Result<CountryResponse>>
{
    public GetCountryByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}