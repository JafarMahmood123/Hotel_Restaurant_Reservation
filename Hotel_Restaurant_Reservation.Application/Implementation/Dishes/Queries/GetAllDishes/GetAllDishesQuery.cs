using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQuery : IQuery<Result<IEnumerable<DishResponse>>>
    {
    }
}