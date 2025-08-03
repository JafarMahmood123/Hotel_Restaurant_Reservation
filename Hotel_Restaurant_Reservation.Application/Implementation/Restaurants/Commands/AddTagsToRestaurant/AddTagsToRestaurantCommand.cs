using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;

public class AddTagsToRestaurantCommand : ICommand<Result<TagResponse>>
{
    public AddTagsToRestaurantCommand(Guid restaurantId, Guid tagId)
    {
        RestaurantId = restaurantId;
        TagId = tagId;
    }

    public Guid RestaurantId { get; }
    public Guid TagId { get; }
}