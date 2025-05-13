using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries.GetAllLocations;

public class GetAllLocationsQueryHandler : IQueryHandler<GetAllLocationsQuery, IEnumerable<Location>?>
{
    private readonly IGenericRepository<Location> genericRepository;

    public GetAllLocationsQueryHandler(IGenericRepository<Location> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<IEnumerable<Location>?> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetAllAsync();
    }
}
