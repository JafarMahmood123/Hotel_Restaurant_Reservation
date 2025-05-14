using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;

public class RemoveTagsFromRestaurantCommand : ICommand<IEnumerable<Tag>>
{
    public RemoveTagsFromRestaurantCommand(Guid restaurantId, IEnumerable<Guid> tagIds)
    {
        RestaurantId = restaurantId;
        TagIds = tagIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> TagIds { get; }
}
