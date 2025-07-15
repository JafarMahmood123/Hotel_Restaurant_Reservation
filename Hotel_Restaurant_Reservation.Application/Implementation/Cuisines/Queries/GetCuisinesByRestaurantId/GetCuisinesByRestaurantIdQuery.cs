using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries.GetCuisinesByRestaurantId
{
    public class GetCuisinesByRestaurantIdQuery : IQuery<Result<IEnumerable<CuisineResponse>>>
    {
        public GetCuisinesByRestaurantIdQuery(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; }
    }
}