using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Tags.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveTagsFromRestaurant;

public class RemoveTagsFromRestaurantCommand : ICommand<Result>
{
    public RemoveTagsFromRestaurantCommand(Guid restaurantId, Guid tagId)
    {
        RestaurantId = restaurantId;
        TagId = tagId;
    }

    public Guid RestaurantId { get; }
    public Guid TagId { get; }
}