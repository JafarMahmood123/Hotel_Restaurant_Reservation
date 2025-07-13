using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryByName;

public class GetCountryByNameQueryHandler : IQueryHandler<GetCountryByNameQuery, Country?>
{
    private readonly IGenericRepository<Country> genericRepository;

    public GetCountryByNameQueryHandler(IGenericRepository<Country> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Country?> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetFirstOrDefaultAsync(x => x.Name == request.Name);
    }
}
