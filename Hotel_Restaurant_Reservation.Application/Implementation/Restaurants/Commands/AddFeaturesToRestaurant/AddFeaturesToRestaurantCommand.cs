using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddFeaturesToRestaurant;

public class AddFeaturesToRestaurantCommand : ICommand<Result<List<FeatureResponse>>>
{
    public AddFeaturesToRestaurantCommand(
        Guid restaurantId,
        AddFeaturesToRestaurantRequest request)
    {
        RestaurantId = restaurantId;
        Request = request;
    }

    public Guid RestaurantId { get; }
    public AddFeaturesToRestaurantRequest Request { get; }
}