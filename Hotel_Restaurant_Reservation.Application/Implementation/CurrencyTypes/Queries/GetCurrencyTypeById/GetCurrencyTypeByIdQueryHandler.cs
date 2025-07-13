using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries.GetCurrencyTypeById;

public class GetCurrencyTypeByIdQueryHandler : IQueryHandler<GetCurrencyTypeByIdQuery, Result<CurrencyTypeResponse>>
{
    private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
    private readonly IMapper _mapper;

    public GetCurrencyTypeByIdQueryHandler(IGenericRepository<CurrencyType> currencyTypeRepository, IMapper mapper)
    {
        _currencyTypeRepository = currencyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<CurrencyTypeResponse>> Handle(GetCurrencyTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var currencyType = await _currencyTypeRepository.GetByIdAsync(request.Id);

        if (currencyType is null)
        {
            return Result.Failure<CurrencyTypeResponse>(DomainErrors.CurrencyType.NotFound(request.Id));
        }

        var currencyTypeResponse = _mapper.Map<CurrencyTypeResponse>(currencyType);
        return Result.Success(currencyTypeResponse);
    }
}