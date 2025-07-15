using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetAllHotelReviewsByUserId
{
    public class GetAllHotelReviewsByUserIdQuery : IQuery<Result<IEnumerable<HotelReviewResponse>>>
    {
        public GetAllHotelReviewsByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
