using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Queries.GetCityById;

public class GetCityByIdQueryHandler : IQueryHandler<GetCityByIdQuery, City?>
{
    private readonly IGenericRepository<City> genericRepository;

    public GetCityByIdQueryHandler(IGenericRepository<City> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<City?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetByIdAsync(request.Id);
    }
}
