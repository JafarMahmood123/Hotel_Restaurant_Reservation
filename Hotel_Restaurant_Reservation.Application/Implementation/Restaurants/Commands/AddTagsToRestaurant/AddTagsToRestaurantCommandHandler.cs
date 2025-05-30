using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;

public class AddTagsToRestaurantCommandHandler
    : ICommandHandler<AddTagsToRestaurantCommand, Result<List<TagResponse>>>
{
    private readonly IGenericRepository<Tag> _tagRepository;
    private readonly IGenericRepository<RestaurantTag> _restaurantTagRepository;
    private readonly IMapper _mapper;

    public AddTagsToRestaurantCommandHandler(
        IGenericRepository<Tag> tagRepository,
        IGenericRepository<RestaurantTag> restaurantTagRepository,
        IMapper mapper)
    {
        _tagRepository = tagRepository;
        _restaurantTagRepository = restaurantTagRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<TagResponse>>> Handle(
        AddTagsToRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var tagIds = request.AddTagsToRestaurantRequest.Ids;

        List<Tag> tags = new();

        // Verify all tags exist
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

        // Create new restaurant-tag associations
        foreach (var tagId in tagIds)
        {
            var existingAssociation = await _restaurantTagRepository.GetFirstOrDefaultAsync(
                x => x.RestaurantId == restaurantId && x.TagId == tagId);

            if (existingAssociation == null)
            {
                var newAssociation = new RestaurantTag
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = restaurantId,
                    TagId = tagId
                };

                await _restaurantTagRepository.AddAsync(newAssociation);
                await _restaurantTagRepository.SaveChangesAsync();
            }            
        }

        // Map to response DTOs
        var response = _mapper.Map<List<TagResponse>>(tags);

        return Result.Success(response);
    }
}