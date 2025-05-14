using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;

public class AddTagsToRestaurantCommand : ICommand<IEnumerable<Tag>>
{
    public AddTagsToRestaurantCommand(Guid restaurantId, IEnumerable<Guid> tagIds)
    {
        RestaurantId = restaurantId;
        TagIds = tagIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> TagIds { get; }
}
