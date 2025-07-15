using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;


namespace Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries.GetTagsByRestaurantId
{
    public class GetTagsByRestaurantIdQueryHandler : IQueryHandler<GetTagsByRestaurantIdQuery, Result<IEnumerable<TagResponse>>>
    {
        private readonly IGenericRepository<RestaurantTag> _restaurantTagRepository;
        private readonly IMapper _mapper;

        public GetTagsByRestaurantIdQueryHandler(IGenericRepository<RestaurantTag> restaurantTagRepository, IMapper mapper)
        {
            _restaurantTagRepository = restaurantTagRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TagResponse>>> Handle(GetTagsByRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            var restaurantTags = await _restaurantTagRepository
                .Where(rt => rt.RestaurantId == request.RestaurantId)
                .Include(rt => rt.Tag)
                .ToListAsync(cancellationToken);

            var tags = restaurantTags.Select(rt => rt.Tag);

            var tagResponses = _mapper.Map<IEnumerable<TagResponse>>(tags);

            return Result.Success(tagResponses);
        }
    }
}