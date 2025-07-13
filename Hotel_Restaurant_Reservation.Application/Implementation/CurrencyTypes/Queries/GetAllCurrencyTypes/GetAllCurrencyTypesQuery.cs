using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries.GetAllCurrencyTypes;

public class GetAllCurrencyTypesQuery : IQuery<Result<IEnumerable<CurrencyTypeResponse>>>
{
}