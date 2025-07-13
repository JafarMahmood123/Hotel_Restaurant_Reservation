using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries.GetAllCurrencyTypes;

public class GetAllCurrencyTypesQueryHandler : IQueryHandler<GetAllCurrencyTypesQuery, Result<IEnumerable<CurrencyTypeResponse>>>
{
    private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
    private readonly IMapper _mapper;

    public GetAllCurrencyTypesQueryHandler(IGenericRepository<CurrencyType> currencyTypeRepository, IMapper mapper)
    {
        _currencyTypeRepository = currencyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CurrencyTypeResponse>>> Handle(GetAllCurrencyTypesQuery request, CancellationToken cancellationToken)
    {
        var currencyTypes = await _currencyTypeRepository.GetAllAsync();
        var currencyTypeResponses = _mapper.Map<IEnumerable<CurrencyTypeResponse>>(currencyTypes);
        return Result.Success(currencyTypeResponses);
    }
}