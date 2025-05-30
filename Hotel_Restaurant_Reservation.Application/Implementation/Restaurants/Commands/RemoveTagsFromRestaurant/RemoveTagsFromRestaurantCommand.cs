using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;

public class RemoveTagsFromRestaurantCommand : ICommand<Result<List<TagResponse>>>
{
    public RemoveTagsFromRestaurantCommand(
        Guid restaurantId,
        RemoveTagsFromRestaurantRequest request)
    {
        RestaurantId = restaurantId;
        Request = request;
    }

    public Guid RestaurantId { get; }
    public RemoveTagsFromRestaurantRequest Request { get; }
}