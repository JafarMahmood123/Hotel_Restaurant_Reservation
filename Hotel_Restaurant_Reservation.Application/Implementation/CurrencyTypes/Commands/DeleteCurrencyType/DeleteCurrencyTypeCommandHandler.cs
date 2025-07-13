using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.DeleteCurrencyType;

public class DeleteCurrencyTypeCommandHandler : ICommandHandler<DeleteCurrencyTypeCommand, Result>
{
    private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;

    public DeleteCurrencyTypeCommandHandler(IGenericRepository<CurrencyType> currencyTypeRepository)
    {
        _currencyTypeRepository = currencyTypeRepository;
    }

    public async Task<Result> Handle(DeleteCurrencyTypeCommand request, CancellationToken cancellationToken)
    {
        var currencyType = await _currencyTypeRepository.GetByIdAsync(request.Id);

        if (currencyType is null)
        {
            return Result.Failure(DomainErrors.CurrencyType.NotFound(request.Id));
        }

        await _currencyTypeRepository.RemoveAsync(request.Id);
        await _currencyTypeRepository.SaveChangesAsync();

        return Result.Success();
    }
}