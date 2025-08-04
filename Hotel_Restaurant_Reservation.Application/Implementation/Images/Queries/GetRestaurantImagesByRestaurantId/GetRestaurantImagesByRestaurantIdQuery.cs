using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetRestaurantImagesByRestaurantId
{
    /// <summary>
    /// Represents the query to get all image URLs for a specific restaurant.
    /// </summary>
    public class GetRestaurantImagesByRestaurantIdQuery : IQuery<Result<List<string>>>
    {
        public GetRestaurantImagesByRestaurantIdQuery(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; }
    }
}
