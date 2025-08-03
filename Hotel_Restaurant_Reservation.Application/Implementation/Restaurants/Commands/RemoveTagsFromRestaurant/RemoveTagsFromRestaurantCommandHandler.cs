using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;

public class RemoveTagsFromRestaurantCommandHandler
    : ICommandHandler<RemoveTagsFromRestaurantCommand, Result>
{
    private readonly IGenericRepository<RestaurantTag> _restaurantTagRepository;
    private readonly IGenericRepository<Tag> _tagRepository;
    private readonly IMapper _mapper;

    public RemoveTagsFromRestaurantCommandHandler(IGenericRepository<RestaurantTag> restaurantTagRepository, IGenericRepository<Tag> tagRepository,
        IMapper mapper)
    {
        _restaurantTagRepository = restaurantTagRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(RemoveTagsFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var tagId = request.TagId;

        var tag = await _tagRepository.GetByIdAsync(tagId);
        if (tag == null)
        {
            return Result.Failure(
                DomainErrors.Tag.NotFound(tagId));
        }

        var restaurantTag = await _restaurantTagRepository
            .GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.TagId == tagId);

        if (restaurantTag == null)
        {
            return Result.Failure<List<TagResponse>>(
                DomainErrors.Restaurant.NoTagsToRemove);
        }

        await _restaurantTagRepository.RemoveAsync(restaurantTag.Id);
        await _restaurantTagRepository.SaveChangesAsync();

        return Result.Success();
    }
}