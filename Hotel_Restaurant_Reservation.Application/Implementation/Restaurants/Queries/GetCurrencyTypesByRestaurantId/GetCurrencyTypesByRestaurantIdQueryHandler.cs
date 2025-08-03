using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetCurrencyTypesByRestaurantId;

public class GetCurrencyTypesByRestaurantIdQueryHandler :
    IQueryHandler<GetCurrencyTypesByRestaurantIdQuery, Result<IEnumerable<GetCurrencyTypesByRestaurantIdResponse>>>
{
    private readonly IGenericRepository<RestaurantCurrencyType> _restaurantCurrencyTypesRepository;
    private readonly IMapper _mapper;

    public GetCurrencyTypesByRestaurantIdQueryHandler(IGenericRepository<RestaurantCurrencyType> restaurantCurrencyTypesRepository,
        IMapper mapper)
    {
        _restaurantCurrencyTypesRepository = restaurantCurrencyTypesRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetCurrencyTypesByRestaurantIdResponse>>> Handle(GetCurrencyTypesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurantCurrencyTypes = await _restaurantCurrencyTypesRepository.Where(x => x.RestaurantId == request.Id)
            .Include(x => x.CurrencyType).ToListAsync();

        var result = _mapper.Map<List<GetCurrencyTypesByRestaurantIdResponse>>(restaurantCurrencyTypes);

        return Result.Success((IEnumerable<GetCurrencyTypesByRestaurantIdResponse>)result);
    }
}
