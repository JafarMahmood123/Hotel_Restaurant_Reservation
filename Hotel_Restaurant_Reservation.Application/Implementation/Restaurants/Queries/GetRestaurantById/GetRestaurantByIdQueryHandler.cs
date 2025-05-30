using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler : IQueryHandler<GetRestaurantByIdQuery, Result<RestaurantResponse>>
{
    private readonly IGenericRepository<Restaurant> _restaurantRepository;
    private readonly IMapper _mapper;

    public GetRestaurantByIdQueryHandler(
        IGenericRepository<Restaurant> restaurantRepository,
        IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<Result<RestaurantResponse>> Handle(
        GetRestaurantByIdQuery request,
        CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

        if (restaurant == null)
        {
            return Result.Failure<RestaurantResponse>(
                DomainErrors.Restaurant.NotFound(request.Id));
        }

        var response = _mapper.Map<RestaurantResponse>(restaurant);
        return Result.Success(response);
    }
}