using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Countries.Queries.GetCountryById;

public class GetCountryByIdQueryHandler : IQueryHandler<GetCountryByIdQuery, Country?>
{
    private readonly IGenericRepository<Country> genericRepository;

    public GetCountryByIdQueryHandler(IGenericRepository<Country> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<Country?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetByIdAsync(request.Id);
    }
}
