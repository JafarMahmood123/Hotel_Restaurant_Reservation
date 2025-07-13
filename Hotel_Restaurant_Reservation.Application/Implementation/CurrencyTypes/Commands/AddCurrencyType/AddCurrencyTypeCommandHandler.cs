using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.AddCurrencyType;

public class AddCurrencyTypeCommandHandler : ICommandHandler<AddCurrencyTypeCommand, Result<CurrencyTypeResponse>>
{
    private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
    private readonly IMapper _mapper;

    public AddCurrencyTypeCommandHandler(IGenericRepository<CurrencyType> currencyTypeRepository, IMapper mapper)
    {
        _currencyTypeRepository = currencyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<CurrencyTypeResponse>> Handle(AddCurrencyTypeCommand request, CancellationToken cancellationToken)
    {
        var currencyType = _mapper.Map<CurrencyType>(request.AddCurrencyTypeRequest);

        var existingCurrencyType = await _currencyTypeRepository.GetFirstOrDefaultAsync(x => x.CurrencyCode == currencyType.CurrencyCode);

        if (existingCurrencyType != null)
            return Result.Failure<CurrencyTypeResponse>(DomainErrors.CurrencyType.NotFound(currencyType.CurrencyCode));

        currencyType.Id = Guid.NewGuid();

        currencyType = await _currencyTypeRepository.AddAsync(currencyType);

        await _currencyTypeRepository.SaveChangesAsync();

        var currencyTypeResponse = _mapper.Map<CurrencyTypeResponse>(currencyType);

        return Result.Success(currencyTypeResponse);
    }
}