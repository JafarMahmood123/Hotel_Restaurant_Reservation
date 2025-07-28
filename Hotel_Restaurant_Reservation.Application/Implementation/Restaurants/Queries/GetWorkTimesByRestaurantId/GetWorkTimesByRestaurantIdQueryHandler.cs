using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetWorkTimesByRestaurantId;

public class GetWorkTimesByRestaurantIdQueryHandler : IQueryHandler<GetWorkTimesByRestaurantIdQuery, Result<IEnumerable<GetWorkTimesByRestaurantIdResponse>>>
{
    private readonly IGenericRepository<RestaurantWorkTime> _restaurantWorkTimeRepository;
    private readonly IMapper _mapper;

    public GetWorkTimesByRestaurantIdQueryHandler(IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository
        , IMapper mapper)
    {
        _restaurantWorkTimeRepository = restaurantWorkTimeRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetWorkTimesByRestaurantIdResponse>>> Handle(GetWorkTimesByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        var restaurantWorkTime = await _restaurantWorkTimeRepository.Where(x => x.RestaurantId == request.RestaurantId)
            .Include(x => x.WorkTime).ToListAsync();

        var result = _mapper.Map<List<GetWorkTimesByRestaurantIdResponse>>(restaurantWorkTime);

        return Result.Success((IEnumerable<GetWorkTimesByRestaurantIdResponse>)result);
    }
}