using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;

public class AddTagsToRestaurantCommandHandler : ICommandHandler<AddTagsToRestaurantCommand, IEnumerable<Tag>>
{
    private readonly IGenericRepository<Tag> tagRepository;
    private readonly IGenericRepository<RestaurantTag> restaurantTagRepository;

    public AddTagsToRestaurantCommandHandler(IGenericRepository<Tag> tagRepository,
        IGenericRepository<RestaurantTag> restaurantTagRepository)
    {
        this.tagRepository = tagRepository;
        this.restaurantTagRepository = restaurantTagRepository;
    }

    public async Task<IEnumerable<Tag>> Handle(AddTagsToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var tagIds = request.TagIds;

        List<RestaurantTag> restaurantTags = new List<RestaurantTag>();

        foreach (var tagId in tagIds)
        {
            var restaurantTag = await restaurantTagRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.TagId == tagId);

            if (restaurantTag == null)
            {

                restaurantTag = new RestaurantTag()
                {
                    Id = Guid.NewGuid(),
                    TagId = tagId,
                    RestaurantId = restaurantId
                };

                restaurantTags.Add(restaurantTag);
            }

        }

        await restaurantTagRepository.AddRangeAsync(restaurantTags);

        await restaurantTagRepository.SaveChangesAsync();


        List<Tag> tags = new List<Tag>();

        foreach (var tagId in tagIds)
        {
            tags.Add(await tagRepository.GetByIdAsync(tagId));
        }

        return tags;
    }
}
