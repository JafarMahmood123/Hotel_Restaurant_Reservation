using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationById;

public class GetLocalLocationByIdQueryHandler : IQueryHandler<GetLocalLocationByIdQuery, LocalLocation?>
{
    private readonly IGenericRepository<LocalLocation> genericRepository;

    public GetLocalLocationByIdQueryHandler(IGenericRepository<LocalLocation> genericRepository)
    {
        this.genericRepository = genericRepository;
    }

    public async Task<LocalLocation?> Handle(GetLocalLocationByIdQuery request, CancellationToken cancellationToken)
    {
        return await genericRepository.GetByIdAsync(request.Id);
    }
}
