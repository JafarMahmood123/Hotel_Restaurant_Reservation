using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.RestaurantReviews.Queries.GetAllRestaurantReviews
{
    public class GetAllRestaurantReviewsQuery : IQuery<Result<IEnumerable<RestaurantReviewResponse>>>
    {
    }
}
