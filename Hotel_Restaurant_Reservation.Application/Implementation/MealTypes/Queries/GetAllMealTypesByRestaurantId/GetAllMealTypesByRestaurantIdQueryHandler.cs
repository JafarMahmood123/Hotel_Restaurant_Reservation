using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries.GetAllMealTypesByRestaurantId
{
    public class GetAllMealTypesByRestaurantIdQueryHandler : IQueryHandler<GetAllMealTypesByRestaurantIdQuery, Result<IEnumerable<MealTypeResponse>>>
    {
        private readonly IGenericRepository<RestaurantMealType> _restaurantMealTypeRepository;
        private readonly IMapper _mapper;

        public GetAllMealTypesByRestaurantIdQueryHandler(IGenericRepository<RestaurantMealType> restaurantMealTypeRepository, IMapper mapper)
        {
            _restaurantMealTypeRepository = restaurantMealTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<MealTypeResponse>>> Handle(GetAllMealTypesByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            var restaurantMealTypes = await _restaurantMealTypeRepository
                .Where(rmt => rmt.RestaurantId == request.RestaurantId)
                .Include(rmt => rmt.MealType)
                .ToListAsync(cancellationToken);

            var mealTypes = restaurantMealTypes.Select(rmt => rmt.MealType);

            var mealTypeResponses = _mapper.Map<IEnumerable<MealTypeResponse>>(mealTypes);

            return Result.Success(mealTypeResponses);
        }
    }
}
