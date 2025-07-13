using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetAllCities;

public class GetAllCitiesQueryHandler : IQueryHandler<GetAllCitiesQuery, IEnumerable<City>?>
{
    private readonly IGenericRepository<City> genericRepository;

    public GetAllCitiesQueryHandler(IGenericRepository<City> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<IEnumerable<City>?> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetAllAsync();
    }
}
