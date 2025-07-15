using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries.GetAllMealTypes
{
    public class GetAllMealTypesQuery : IQuery<Result<IEnumerable<MealTypeResponse>>>
    {
    }
}