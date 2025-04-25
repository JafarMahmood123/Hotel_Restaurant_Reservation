using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler : IQueryHandler<GetAllRestaurantsQuery, IEnumerable<Restaurant>?>
{
    private readonly IGenericRepository<Restaurant> _genericRepository;

    public GetAllRestaurantsQueryHandler(IGenericRepository<Restaurant> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<Restaurant>?> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        return await _genericRepository.GetAllAsync();
    }
}
