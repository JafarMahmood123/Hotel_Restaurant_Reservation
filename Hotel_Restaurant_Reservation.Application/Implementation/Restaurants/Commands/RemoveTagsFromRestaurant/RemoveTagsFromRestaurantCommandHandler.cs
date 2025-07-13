using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;

public class RemoveTagsFromRestaurantCommandHandler
    : ICommandHandler<RemoveTagsFromRestaurantCommand, Result<List<TagResponse>>>
{
    private readonly IGenericRepository<RestaurantTag> _restaurantTagRepository;
    private readonly IGenericRepository<Tag> _tagRepository;
    private readonly IMapper _mapper;

    public RemoveTagsFromRestaurantCommandHandler(
        IGenericRepository<RestaurantTag> restaurantTagRepository,
        IGenericRepository<Tag> tagRepository,
        IMapper mapper)
    {
        _restaurantTagRepository = restaurantTagRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<TagResponse>>> Handle(
        RemoveTagsFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var tagIds = request.Request.Ids;

        // Verify all tags exist
        var tags = new List<Tag>();
        foreach (var tagId in tagIds)
        {
            var tag = await _tagRepository.GetByIdAsync(tagId);
            if (tag == null)
            {
                return Result.Failure<List<TagResponse>>(
                    DomainErrors.Tag.NotFound(tagId));
            }
            tags.Add(tag);
        }

        // Get all existing associations
        var restaurantTags = await _restaurantTagRepository
            .Where(x => x.RestaurantId == restaurantId && tagIds.Contains(x.TagId))
            .ToListAsync();

        if (!restaurantTags.Any())
        {
            return Result.Failure<List<TagResponse>>(
                DomainErrors.Restaurant.NoTagsToRemove);
        }

        // Remove associations
        _restaurantTagRepository.RemoveRange(restaurantTags);
        await _restaurantTagRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<List<TagResponse>>(tags);
        return Result.Success(response);
    }
}