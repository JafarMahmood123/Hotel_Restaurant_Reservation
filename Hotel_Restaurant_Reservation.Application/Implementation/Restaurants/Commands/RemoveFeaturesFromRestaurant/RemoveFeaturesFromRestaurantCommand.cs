using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.FeatureDTOs;
using Hotel_Restaurant_Reservation.Application.Implementation.Features.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveFeaturesFromRestaurant;

public class RemoveFeaturesFromRestaurantCommand : ICommand<Result<List<FeatureResponse>>>
{
    public RemoveFeaturesFromRestaurantCommand(
        Guid restaurantId,
        RemoveFeaturesFromRestaurantRequest removeFeaturesFromRestaurantRequest)
    {
        RestaurantId = restaurantId;
        RemoveFeaturesFromRestaurantRequest = removeFeaturesFromRestaurantRequest;
    }

    public Guid RestaurantId { get; }
    public RemoveFeaturesFromRestaurantRequest RemoveFeaturesFromRestaurantRequest { get; }
}