using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries.GetDishesWithPricesByRestaurantId;

public class GetDishesWithPricesByRestaurantIdQueryHandler : IQueryHandler<GetDishesWithPricesByRestaurantIdQuery, Result<IEnumerable<DishWithPriceResponse>>>
{
    private readonly IGenericRepository<RestaurantDishPrice> _restaurantDishPriceRepository;
    private readonly IMapper _mapper;

    public GetDishesWithPricesByRestaurantIdQueryHandler(IGenericRepository<RestaurantDishPrice> restaurantDishPriceRepository, IMapper mapper)
    {
        _restaurantDishPriceRepository = restaurantDishPriceRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<DishWithPriceResponse>>> Handle(GetDishesWithPricesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurantDishPrices = await _restaurantDishPriceRepository
            .Where(rdp => rdp.RestaurantId == request.RestaurantId)
            .Include(rdp => rdp.Dish)
            .ToListAsync(cancellationToken);

        var dishWithPriceResponses = restaurantDishPrices.Select(rdp => new DishWithPriceResponse
        {
            Dish = _mapper.Map<DishResponse>(rdp.Dish),
            Price = rdp.Price
        });

        return Result.Success(dishWithPriceResponses);
    }
}