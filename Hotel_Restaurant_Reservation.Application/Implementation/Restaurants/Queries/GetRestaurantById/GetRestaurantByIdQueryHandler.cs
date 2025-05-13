using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetHotelById;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Threading.Tasks.Sources;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler : IQueryHandler<GetRestaurantByIdQuery, Restaurant?>
{
    private readonly IGenericRepository<Restaurant> _genericRepository;

    public GetRestaurantByIdQueryHandler(IGenericRepository<Restaurant> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Restaurant?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        return await _genericRepository.GetByIdAsync(request.Id);
    }
}
