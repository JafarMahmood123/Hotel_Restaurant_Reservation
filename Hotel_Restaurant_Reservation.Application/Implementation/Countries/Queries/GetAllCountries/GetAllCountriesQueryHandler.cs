using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Microsoft.VisualBasic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetAllCountries;

public class GetAllCountriesQueryHandler : IQueryHandler<GetAllCountriesQuery, IEnumerable<Country>?>
{
    private readonly IGenericRepository<Country> genericRepository;

    public GetAllCountriesQueryHandler(IGenericRepository<Country> genericRepository)
    {
        this.genericRepository = genericRepository;
    }
    public async Task<IEnumerable<Country>?> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetAllAsync();
    }
}
