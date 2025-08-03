using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries.GetDishesWithPricesByRestaurantId;

public class GetDishesWithPricesByRestaurantIdQueryHandler : IQueryHandler<GetDishesWithPricesByRestaurantIdQuery, Result<IEnumerable<RestaurantDishResponse>>>
{
    private readonly IGenericRepository<RestaurantDish> _restaurantDishPriceRepository;
    private readonly IMapper _mapper;

    public GetDishesWithPricesByRestaurantIdQueryHandler(IGenericRepository<RestaurantDish> restaurantDishPriceRepository, IMapper mapper)
    {
        _restaurantDishPriceRepository = restaurantDishPriceRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<RestaurantDishResponse>>> Handle(GetDishesWithPricesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurantDishPrices = await _restaurantDishPriceRepository
            .Where(rdp => rdp.RestaurantId == request.RestaurantId)
            .Include(rdp => rdp.Dish)
            .ToListAsync(cancellationToken);

        var dishWithPriceResponses = restaurantDishPrices.Select(rdp => new RestaurantDishResponse
        {
            Id = rdp.DishId,
            Price = rdp.Price,
            Description = rdp.Description,
            Name = rdp.Dish.Name,
            PictureUrl = rdp.PictureUrl,
        });
            
        return Result.Success(dishWithPriceResponses);
    }
}