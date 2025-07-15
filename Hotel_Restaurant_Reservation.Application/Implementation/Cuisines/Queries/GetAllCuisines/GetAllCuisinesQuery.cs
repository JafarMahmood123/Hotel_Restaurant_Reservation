using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Collections.Generic;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries.GetAllCuisines
{
    public class GetAllCuisinesQuery : IQuery<Result<IEnumerable<CuisineResponse>>>
    {
    }
}