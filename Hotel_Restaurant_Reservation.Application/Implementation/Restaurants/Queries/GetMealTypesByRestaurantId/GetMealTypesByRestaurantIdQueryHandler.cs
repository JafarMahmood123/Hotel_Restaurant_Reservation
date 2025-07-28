using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetMealTypesByRestaurantId;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

public class GetMealTypesByRestaurantIdQueryHandler : IQueryHandler<GetMealTypesByRestaurantIdQuery, Result<IEnumerable<GetMealTypesByRestaurantIdResponse>>>
{
    private readonly IGenericRepository<RestaurantMealType> _restaurantMealTypeRepository;
    private readonly IMapper _mapper;

    public GetMealTypesByRestaurantIdQueryHandler(IGenericRepository<RestaurantMealType> restaurantMealTypeRepository, IMapper mapper)
    {
        _restaurantMealTypeRepository = restaurantMealTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetMealTypesByRestaurantIdResponse>>> Handle(GetMealTypesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var mealTypes = await _restaurantMealTypeRepository.Where(x => x.RestaurantId == request.RestaurantId)
            .Include(x => x.MealType).ToListAsync();

        var result = _mapper.Map<List<GetMealTypesByRestaurantIdResponse>>(mealTypes);

        return Result.Success((IEnumerable<GetMealTypesByRestaurantIdResponse>)result);
    }
}