using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries.GetCuisinesByRestaurantId
{
    public class GetCuisinesByRestaurantIdQueryHandler : IQueryHandler<GetCuisinesByRestaurantIdQuery, Result<IEnumerable<CuisineResponse>>>
    {
        private readonly IGenericRepository<RestaurantCuisine> _restaurantCuisineRepository;
        private readonly IMapper _mapper;

        public GetCuisinesByRestaurantIdQueryHandler(IGenericRepository<RestaurantCuisine> restaurantCuisineRepository, IMapper mapper)
        {
            _restaurantCuisineRepository = restaurantCuisineRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CuisineResponse>>> Handle(GetCuisinesByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            var restaurantCuisines = await _restaurantCuisineRepository
                .Where(rc => rc.RestaurantId == request.RestaurantId)
                .Include(rc => rc.Cuisine)
                .ToListAsync(cancellationToken);

            var cuisines = restaurantCuisines.Select(rc => rc.Cuisine);

            var cuisineResponses = _mapper.Map<IEnumerable<CuisineResponse>>(cuisines);

            return Result.Success(cuisineResponses);
        }
    }
}