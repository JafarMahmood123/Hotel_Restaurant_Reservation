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
        var restaurantWorkTimes = await _restaurantWorkTimeRepository.Where(x => x.RestaurantId == request.RestaurantId)
            .Include(x => x.WorkTime)
            .ToListAsync(cancellationToken);

        // Select the nested WorkTime objects before mapping
        var workTimes = restaurantWorkTimes.Select(rwt => rwt.WorkTime);

        // Now map the correct collection
        var result = _mapper.Map<IEnumerable<GetWorkTimesByRestaurantIdResponse>>(workTimes);

        return Result.Success(result);
    }
}