using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.WorkTimes.Queries.GetWorkTimesByRestaurantId
{
    public class GetWorkTimesByRestaurantIdQueryHandler : IQueryHandler<GetWorkTimesByRestaurantIdQuery, Result<IEnumerable<WorkTimeResponse>>>
    {
        private readonly IGenericRepository<RestaurantWorkTime> _restaurantWorkTimeRepository;
        private readonly IMapper _mapper;

        public GetWorkTimesByRestaurantIdQueryHandler(IGenericRepository<RestaurantWorkTime> restaurantWorkTimeRepository, IMapper mapper)
        {
            _restaurantWorkTimeRepository = restaurantWorkTimeRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<WorkTimeResponse>>> Handle(GetWorkTimesByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            var restaurantWorkTimes = await _restaurantWorkTimeRepository
                .Where(rwt => rwt.RestaurantId == request.RestaurantId)
                .Include(rwt => rwt.WorkTime)
                .ToListAsync(cancellationToken);

            var workTimes = restaurantWorkTimes.Select(rwt => rwt.WorkTime);

            var workTimeResponses = _mapper.Map<IEnumerable<WorkTimeResponse>>(workTimes);

            return Result.Success(workTimeResponses);
        }
    }
}