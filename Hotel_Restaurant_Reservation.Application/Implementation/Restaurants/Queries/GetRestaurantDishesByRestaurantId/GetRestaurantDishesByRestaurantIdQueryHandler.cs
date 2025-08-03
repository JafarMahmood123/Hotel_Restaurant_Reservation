using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetRestaurantDishesByRestaurantId;

public class GetRestaurantDishesByRestaurantIdQueryHandler : ICommandHandler<GetRestaurantDishesByRestaurantIdQuery, Result<IEnumerable<RestaurantDishResponse>>>
{
    private readonly IRestaurantRespository _restaurantRepository;
    private readonly IGenericRepository<RestaurantDish> _restaurantDishPrice;

    public GetRestaurantDishesByRestaurantIdQueryHandler(IRestaurantRespository restaurantRepository,
        IGenericRepository<RestaurantDish> restaurantDishPrice)
    {
        _restaurantRepository = restaurantRepository;
        _restaurantDishPrice = restaurantDishPrice;
    }

    public async Task<Result<IEnumerable<RestaurantDishResponse>>> Handle(GetRestaurantDishesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
        {
            return Result.Failure<IEnumerable<RestaurantDishResponse>>
                (DomainErrors.Restaurant.NotFound(request.RestaurantId));
        }

        var dishResponses = new List<RestaurantDishResponse>();

        var dishesWithInfo = await _restaurantDishPrice.Where(x => x.RestaurantId == request.RestaurantId)
            .Include(x => x.Dish).ToListAsync();

        foreach (var dishWithInfo in dishesWithInfo)
        {
            dishResponses.Add(new RestaurantDishResponse {
                Id = dishWithInfo.Dish.Id,
                Name = dishWithInfo.Dish.Name,
                Description = dishWithInfo.Description,
                Price = dishWithInfo.Price,
                PictureUrl = dishWithInfo.PictureUrl,
                RestaurantId = dishWithInfo.RestaurantId,
            });
        }

        return Result.Success((IEnumerable<RestaurantDishResponse>)dishResponses);
    }
}