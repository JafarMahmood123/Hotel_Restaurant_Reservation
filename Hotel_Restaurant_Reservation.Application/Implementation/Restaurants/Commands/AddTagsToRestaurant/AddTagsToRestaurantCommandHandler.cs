using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;

public class AddTagsToRestaurantCommandHandler
    : ICommandHandler<AddTagsToRestaurantCommand, Result<TagResponse>>
{
    private readonly IGenericRepository<Tag> _tagRepository;
    private readonly IGenericRepository<RestaurantTag> _restaurantTagRepository;
    private readonly IMapper _mapper;

    public AddTagsToRestaurantCommandHandler(IGenericRepository<Tag> tagRepository, IGenericRepository<RestaurantTag> restaurantTagRepository,
        IMapper mapper)
    {
        _tagRepository = tagRepository;
        _restaurantTagRepository = restaurantTagRepository;
        _mapper = mapper;
    }

    public async Task<Result<TagResponse>> Handle(AddTagsToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var tagId = request.TagId;

        var tag = await _tagRepository.GetByIdAsync(tagId);
        if (tag == null)
        {
            return Result.Failure<TagResponse>(
                DomainErrors.Tag.NotFound(tagId));
        }

        var existingAssociation = await _restaurantTagRepository.GetFirstOrDefaultAsync(
                x => x.RestaurantId == restaurantId && x.TagId == tagId);

        if (existingAssociation != null)
        {
            
        }

        var newAssociation = new RestaurantTag
        {
            Id = Guid.NewGuid(),
            RestaurantId = restaurantId,
            TagId = tagId
        };

        await _restaurantTagRepository.AddAsync(newAssociation);
        await _restaurantTagRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<TagResponse>(tag);

        return Result.Success(response);
    }
}