using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries.GetAllMealTypesByRestaurantId
{
    public class GetAllMealTypesByRestaurantIdQuery : IQuery<Result<IEnumerable<MealTypeResponse>>>
    {
        public GetAllMealTypesByRestaurantIdQuery(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; }
    }
}
