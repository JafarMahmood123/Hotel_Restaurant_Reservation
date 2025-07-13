using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCurrencyTypesFromRestaurant;

public class RemoveCurrencyTypesFromRestaurantCommandHandler
    : ICommandHandler<RemoveCurrencyTypesFromRestaurantCommand, Result<List<CurrencyTypeResponse>>>
{
    private readonly IGenericRepository<RestaurantCurrencyType> _restaurantCurrencyTypeRepository;
    private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
    private readonly IMapper _mapper;

    public RemoveCurrencyTypesFromRestaurantCommandHandler(
        IGenericRepository<RestaurantCurrencyType> restaurantCurrencyTypeRepository,
        IGenericRepository<CurrencyType> currencyTypeRepository,
        IMapper mapper)
    {
        _restaurantCurrencyTypeRepository = restaurantCurrencyTypeRepository;
        _currencyTypeRepository = currencyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CurrencyTypeResponse>>> Handle(
        RemoveCurrencyTypesFromRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var currencyTypeIds = request.RemoveCurrencyTypesFromRestaurantRequest.Ids;

        // Verify all currency types exist
        var currencyTypes = new List<CurrencyType>();
        foreach (var currencyTypeId in currencyTypeIds)
        {
            var currencyType = await _currencyTypeRepository.GetByIdAsync(currencyTypeId);
            if (currencyType == null)
            {
                return Result.Failure<List<CurrencyTypeResponse>>(
                    DomainErrors.CurrencyType.NotFound(currencyTypeId));
            }
            currencyTypes.Add(currencyType);
        }

        // Get all existing associations
        var restaurantCurrencyTypes = await _restaurantCurrencyTypeRepository
            .Where(x => x.RestaurantId == restaurantId && currencyTypeIds.Contains(x.CurrencyTypeId))
            .ToListAsync();

        if (!restaurantCurrencyTypes.Any())
        {
            return Result.Failure<List<CurrencyTypeResponse>>(
                DomainErrors.Restaurant.NoCurrencyTypesToRemove);
        }

        // Remove associations
        _restaurantCurrencyTypeRepository.RemoveRange(restaurantCurrencyTypes);
        await _restaurantCurrencyTypeRepository.SaveChangesAsync();

        // Map to response DTOs
        var response = _mapper.Map<List<CurrencyTypeResponse>>(currencyTypes);
        return Result.Success(response);
    }
}