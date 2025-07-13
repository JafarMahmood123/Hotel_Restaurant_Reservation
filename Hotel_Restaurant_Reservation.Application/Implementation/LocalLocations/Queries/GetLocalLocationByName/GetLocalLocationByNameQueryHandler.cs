using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationByName;

public class GetLocalLocationByNameQueryHandler : IQueryHandler<GetLocalLocationByNameQuery, LocalLocation?>
{
    private readonly IGenericRepository<LocalLocation> genericRepository;

    public GetLocalLocationByNameQueryHandler(IGenericRepository<LocalLocation> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<LocalLocation?> Handle(GetLocalLocationByNameQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetFirstOrDefaultAsync(x => x.Name == request.Name);
    }
}
