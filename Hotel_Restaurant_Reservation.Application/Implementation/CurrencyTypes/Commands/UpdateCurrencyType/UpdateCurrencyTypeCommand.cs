using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.UpdateCurrencyType;

public class UpdateCurrencyTypeCommand : ICommand<Result<CurrencyTypeResponse>>
{
    public UpdateCurrencyTypeCommand(Guid id, UpdateCurrencyTypeRequest updateCurrencyTypeRequest)
    {
        Id = id;
        UpdateCurrencyTypeRequest = updateCurrencyTypeRequest;
    }

    public Guid Id { get; }
    public UpdateCurrencyTypeRequest UpdateCurrencyTypeRequest { get; }
}