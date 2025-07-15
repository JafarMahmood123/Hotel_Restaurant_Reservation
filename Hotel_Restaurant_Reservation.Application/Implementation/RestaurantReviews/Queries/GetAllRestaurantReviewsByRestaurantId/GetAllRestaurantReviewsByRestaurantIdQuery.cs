using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries.GetAllRestaurantReviewsByRestaurantId
{
    public class GetAllRestaurantReviewsByRestaurantIdQuery : IQuery<Result<IEnumerable<RestaurantReviewResponse>>>
    {
        public GetAllRestaurantReviewsByRestaurantIdQuery(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; }
    }
}
