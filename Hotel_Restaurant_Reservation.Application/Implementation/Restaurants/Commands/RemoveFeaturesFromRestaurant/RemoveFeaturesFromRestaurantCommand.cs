using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveFeaturesFromRestaurant;

public class RemoveFeaturesFromRestaurantCommand : ICommand<IEnumerable<Feature>>
{
    public RemoveFeaturesFromRestaurantCommand(Guid restaurantId, IEnumerable<Guid> FeatureIds)
    {
        RestaurantId = restaurantId;
        this.FeatureIds = FeatureIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> FeatureIds { get; }
}
