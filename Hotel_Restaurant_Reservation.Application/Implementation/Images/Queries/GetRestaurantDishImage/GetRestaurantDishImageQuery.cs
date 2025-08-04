using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantDishImage
{
    /// <summary>
    /// Represents the query to get an image URL for a specific dish.
    /// </summary>
    public class GetRestaurantDishImageQuery : IQuery<Result<string>>
    {
        public GetRestaurantDishImageQuery(Guid restaurantId, Guid dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }

        public Guid RestaurantId { get; }
        public Guid DishId { get; }
    }
}