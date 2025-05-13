using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;


// Need Editing Later
public class GetHotelByIdQueryHandler : IQueryHandler<GetHotelByIdQuery, Hotel?>
{
    private readonly IGenericRepository<Hotel> _genericRepository;

    public GetHotelByIdQueryHandler(IGenericRepository<Hotel> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Hotel?> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
       return await _genericRepository.GetByIdAsync(request.Id);
    }
}
