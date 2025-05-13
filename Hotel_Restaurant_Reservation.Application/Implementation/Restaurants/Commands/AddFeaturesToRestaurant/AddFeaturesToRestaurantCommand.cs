using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;

public class AddFeaturesToRestaurantCommand : ICommand<IEnumerable<Feature>>
{
    public AddFeaturesToRestaurantCommand(Guid restaurantId, IEnumerable<Guid> featureIds)
    {
        RestaurantId = restaurantId;
        FeatureIds = featureIds;
    }

    public Guid RestaurantId { get; }
    public IEnumerable<Guid> FeatureIds { get; }
}
