using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;

public class AddFeaturesToRestaurantCommand : ICommand<Result<FeatureResponse>>
{
    public AddFeaturesToRestaurantCommand(Guid restaurantId, Guid featureId)
    {
        RestaurantId = restaurantId;
        FeatureId = featureId;
    }

    public Guid RestaurantId { get; }
    public Guid FeatureId { get; }
}