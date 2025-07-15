using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQueryHandler : IQueryHandler<GetAllDishesQuery, Result<IEnumerable<DishResponse>>>
    {
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IMapper _mapper;

        public GetAllDishesQueryHandler(IGenericRepository<Dish> dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DishResponse>>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository.GetAllAsync();
            var dishResponses = _mapper.Map<IEnumerable<DishResponse>>(dishes);
            return Result.Success(dishResponses);
        }
    }
}