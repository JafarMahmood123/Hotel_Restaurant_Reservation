using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantCuisinesByRestaurantId;

public class GetRestaurantCuisinesByRestaurantIdQueryHandler :
    IQueryHandler<GetRestaurantCuisinesByRestaurantIdQuery, Result<IEnumerable<GetRestaurantCuisinesByRestaurantIdResponse>>>
{
    private readonly IGenericRepository<RestaurantCuisine> _restaurantCuisineRepository;
    private readonly IMapper _mapper;

    public GetRestaurantCuisinesByRestaurantIdQueryHandler(IGenericRepository<RestaurantCuisine> restaurantCuisineRepository
        ,IMapper mapper)
    {
        _restaurantCuisineRepository = restaurantCuisineRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetRestaurantCuisinesByRestaurantIdResponse>>> Handle(GetRestaurantCuisinesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurantCuisines = await _restaurantCuisineRepository
            .Where(x => x.RestaurantId == request.RestaurantId).Include(x => x.Cuisine).ToListAsync();

        var result = _mapper.Map<List<GetRestaurantCuisinesByRestaurantIdResponse>>(restaurantCuisines);

        return Result.Success((IEnumerable<GetRestaurantCuisinesByRestaurantIdResponse>)result);
    }
}
