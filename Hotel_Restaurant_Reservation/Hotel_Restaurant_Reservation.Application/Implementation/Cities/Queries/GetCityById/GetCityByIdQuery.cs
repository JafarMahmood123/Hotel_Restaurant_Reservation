using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityById;

public class GetCityByIdQuery : IQuery<City?>
{
    public Guid Id { get; set; }

    public GetCityByIdQuery(Guid id)
    {
        Id = id; 
    }
}
