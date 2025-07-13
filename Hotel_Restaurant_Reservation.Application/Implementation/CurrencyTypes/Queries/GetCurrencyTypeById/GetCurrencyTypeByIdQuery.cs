using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries.GetCurrencyTypeById;

public class GetCurrencyTypeByIdQuery : IQuery<Result<CurrencyTypeResponse>>
{
    public GetCurrencyTypeByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}