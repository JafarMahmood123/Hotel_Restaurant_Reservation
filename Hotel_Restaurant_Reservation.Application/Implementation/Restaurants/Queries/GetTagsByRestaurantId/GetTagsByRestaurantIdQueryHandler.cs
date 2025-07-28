using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.Queries.GetTagsByRestaurantId;

internal sealed class GetTagsByRestaurantIdQueryHandler
    : IQueryHandler<GetTagsByRestaurantIdQuery, Result<IEnumerable<GetTagsByRestaurantIdResponse>>>
{
    private readonly IGenericRepository<RestaurantTag> _restaurantTagRepository;
    private readonly IMapper _mapper;

    public GetTagsByRestaurantIdQueryHandler(IGenericRepository<RestaurantTag> restaurantTagRepository, IMapper mapper)
    {
        _restaurantTagRepository = restaurantTagRepository;
        this._mapper = mapper;
    }

    public async Task<Result<IEnumerable<GetTagsByRestaurantIdResponse>>> Handle(
        GetTagsByRestaurantIdQuery request,
        CancellationToken cancellationToken)
    {
        var restaurantTags = await _restaurantTagRepository.Where(x => x.RestaurantId == request.RestaurantId)
            .Include(x => x.Tag).ToListAsync();

        var result = _mapper.Map<List<GetTagsByRestaurantIdResponse>>(restaurantTags);

        return Result.Success((IEnumerable<GetTagsByRestaurantIdResponse>)result);
    }
}