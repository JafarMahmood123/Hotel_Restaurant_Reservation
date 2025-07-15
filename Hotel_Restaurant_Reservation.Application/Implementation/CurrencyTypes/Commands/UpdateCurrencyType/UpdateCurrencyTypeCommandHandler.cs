using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Commands.UpdateCurrencyType
{
    public class UpdateCurrencyTypeCommandHandler : ICommandHandler<UpdateCurrencyTypeCommand, Result<CurrencyTypeResponse>>
    {
        private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
        private readonly IMapper _mapper;

        public UpdateCurrencyTypeCommandHandler(IGenericRepository<CurrencyType> currencyTypeRepository, IMapper mapper)
        {
            _currencyTypeRepository = currencyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<CurrencyTypeResponse>> Handle(UpdateCurrencyTypeCommand request, CancellationToken cancellationToken)
        {
            var currencyType = await _currencyTypeRepository.GetByIdAsync(request.Id);

            if (currencyType is null)
            {
                return Result.Failure<CurrencyTypeResponse>(DomainErrors.CurrencyType.NotFound(request.Id));
            }

            if (currencyType.CurrencyCode == request.UpdateCurrencyTypeRequest.CurrencyCode)
            {
                return Result.Failure<CurrencyTypeResponse>(DomainErrors.CurrencyType.SameCurrencyCode);
            }

            var existingCurrencyType = await _currencyTypeRepository.GetFirstOrDefaultAsync(
                x => x.CurrencyCode == request.UpdateCurrencyTypeRequest.CurrencyCode && x.Id != request.Id);
            if (existingCurrencyType != null)
            {
                return Result.Failure<CurrencyTypeResponse>(DomainErrors.CurrencyType.ExistingCurrencyType(request.UpdateCurrencyTypeRequest.CurrencyCode));
            }


            _mapper.Map(request.UpdateCurrencyTypeRequest, currencyType);

            await _currencyTypeRepository.UpdateAsync(request.Id, currencyType);
            await _currencyTypeRepository.SaveChangesAsync();

            var currencyTypeResponse = _mapper.Map<CurrencyTypeResponse>(currencyType);

            return Result.Success(currencyTypeResponse);
        }
    }
}