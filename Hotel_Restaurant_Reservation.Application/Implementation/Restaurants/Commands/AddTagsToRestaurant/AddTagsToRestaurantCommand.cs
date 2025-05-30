using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddTagsToRestaurant;

public class AddTagsToRestaurantCommand : ICommand<Result<List<TagResponse>>>
{
    public AddTagsToRestaurantCommand(
        Guid restaurantId,
        AddTagsToRestaurantRequest addTagsToRestaurantRequest)
    {
        RestaurantId = restaurantId;
        AddTagsToRestaurantRequest = addTagsToRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public AddTagsToRestaurantRequest AddTagsToRestaurantRequest { get; }
}