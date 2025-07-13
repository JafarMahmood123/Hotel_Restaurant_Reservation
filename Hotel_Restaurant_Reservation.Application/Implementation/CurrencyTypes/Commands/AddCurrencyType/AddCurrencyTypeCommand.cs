using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.AddCurrencyType;

public class AddCurrencyTypeCommand : ICommand<Result<CurrencyTypeResponse>>
{
    public AddCurrencyTypeCommand(AddCurrencyTypeRequest addCurrencyTypeRequest)
    {
        AddCurrencyTypeRequest = addCurrencyTypeRequest;
    }

    public AddCurrencyTypeRequest AddCurrencyTypeRequest { get; }
}