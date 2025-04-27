using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetAllLocalLocations;

public class GetAllLocalLocationsQueryHandler : IQueryHandler<GetAllLocalLocationsQuery, IEnumerable<LocalLocation>?>
{
    private readonly IGenericRepository<LocalLocation> genericRepository;

    public GetAllLocalLocationsQueryHandler(IGenericRepository<LocalLocation> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<IEnumerable<LocalLocation>?> Handle(GetAllLocalLocationsQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetAllAsync();
    }
}
