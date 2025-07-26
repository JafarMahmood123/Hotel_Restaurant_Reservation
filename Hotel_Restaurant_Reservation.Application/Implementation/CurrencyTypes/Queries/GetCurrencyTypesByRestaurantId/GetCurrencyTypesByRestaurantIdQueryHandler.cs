using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries.GetCurrencyTypesByRestaurantId
{
    public class GetCurrencyTypesByRestaurantIdQueryHandler : IQueryHandler<GetCurrencyTypesByRestaurantIdQuery, Result<IEnumerable<CurrencyTypeResponse>>>
    {
        private readonly IGenericRepository<RestaurantCurrencyType> _restaurantCurrencyTypeRepository;
        private readonly IMapper _mapper;

        public GetCurrencyTypesByRestaurantIdQueryHandler(IGenericRepository<RestaurantCurrencyType> restaurantCurrencyTypeRepository, IMapper mapper)
        {
            _restaurantCurrencyTypeRepository = restaurantCurrencyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CurrencyTypeResponse>>> Handle(GetCurrencyTypesByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            var restaurantCurrencyTypes = await _restaurantCurrencyTypeRepository
                .Where(rct => rct.RestaurantId == request.RestaurantId)
                .Include(rct => rct.CurrencyType)
                .ToListAsync(cancellationToken);

            var currencyTypes = restaurantCurrencyTypes.Select(rct => rct.CurrencyType);

            var currencyTypeResponses = _mapper.Map<IEnumerable<CurrencyTypeResponse>>(currencyTypes);

            return Result.Success(currencyTypeResponses);
        }
    }
}