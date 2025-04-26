using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryById;

public class GetCountryByIdQuery : IQuery<Country>
{
    public Guid Id { get; set; }

    public GetCountryByIdQuery(Guid id)
    {
        Id = id;
    }
}
