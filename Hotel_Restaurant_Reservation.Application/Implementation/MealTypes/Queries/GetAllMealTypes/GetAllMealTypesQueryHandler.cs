using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries.GetAllMealTypes
{
    public class GetAllMealTypesQueryHandler : IQueryHandler<GetAllMealTypesQuery, Result<IEnumerable<MealTypeResponse>>>
    {
        private readonly IGenericRepository<MealType> _mealTypeRepository;
        private readonly IMapper _mapper;

        public GetAllMealTypesQueryHandler(IGenericRepository<MealType> mealTypeRepository, IMapper mapper)
        {
            _mealTypeRepository = mealTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<MealTypeResponse>>> Handle(GetAllMealTypesQuery request, CancellationToken cancellationToken)
        {
            var mealTypes = await _mealTypeRepository.GetAllAsync();
            var mealTypeResponses = _mapper.Map<IEnumerable<MealTypeResponse>>(mealTypes);
            return Result.Success(mealTypeResponses);
        }
    }
}