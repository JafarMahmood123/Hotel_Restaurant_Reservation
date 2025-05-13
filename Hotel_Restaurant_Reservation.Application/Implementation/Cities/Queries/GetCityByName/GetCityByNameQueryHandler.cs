using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityByName;

public class GetCityByNameQueryHandler : IQueryHandler<GetCityByNameQuery, City?>
{
    private readonly IGenericRepository<City> genericRepository;

    public GetCityByNameQueryHandler(IGenericRepository<City> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<City?> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetFirstOrDefaultAsync(x => x.Name == request.Name);
    }
}
