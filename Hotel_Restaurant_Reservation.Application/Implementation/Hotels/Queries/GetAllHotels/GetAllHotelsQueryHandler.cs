using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;

public class GetAllHotelsQueryHandler : IQueryHandler<GetAllHotelsQuery, IEnumerable<Hotel>?>
{
    private readonly IGenericRepository<Hotel> _genericRepository;

    public GetAllHotelsQueryHandler(IGenericRepository<Hotel> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<Hotel>?> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        return await _genericRepository.GetAllAsync();
    }
}
