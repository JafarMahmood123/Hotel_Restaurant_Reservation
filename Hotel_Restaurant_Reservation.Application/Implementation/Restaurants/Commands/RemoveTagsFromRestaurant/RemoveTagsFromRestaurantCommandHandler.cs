using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;

public class RemoveTagsFromRestaurantCommandHandler : ICommandHandler<RemoveTagsFromRestaurantCommand, IEnumerable<Tag>>
{
    private readonly IGenericRepository<RestaurantTag> restaurantTagRepository;
    private readonly IGenericRepository<Tag> tagRepository;

    public RemoveTagsFromRestaurantCommandHandler(IGenericRepository<RestaurantTag> restaurantTagRepository,
        IGenericRepository<Tag> tagRepository)
    {
        this.restaurantTagRepository = restaurantTagRepository;
        this.tagRepository = tagRepository;
    }

    public async Task<IEnumerable<Tag>> Handle(RemoveTagsFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var tagIds = request.TagIds;


        List<RestaurantTag> restaurantTags = new List<RestaurantTag>();
        foreach (var tagId in tagIds)
        {
            var restaurantTag = await restaurantTagRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.TagId == tagId);

            restaurantTags.Add(restaurantTag);
        }

        restaurantTagRepository.RemoveRange(restaurantTags);

        await restaurantTagRepository.SaveChangesAsync();

        List<Tag> tags = new List<Tag>();

        foreach (var tagId in tagIds)
        {
            tags.Add(await tagRepository.GetByIdAsync(tagId));
        }

        return tags;
    }
}
