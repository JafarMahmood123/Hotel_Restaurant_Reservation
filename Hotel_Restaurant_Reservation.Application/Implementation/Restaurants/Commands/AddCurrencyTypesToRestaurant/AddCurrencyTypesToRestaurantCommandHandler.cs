﻿using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.CurrencyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCurrencyTypesToRestaurant;

public class AddCurrencyTypesToRestaurantCommandHandler
    : ICommandHandler<AddCurrencyTypesToRestaurantCommand, Result<List<CurrencyTypeResponse>>>
{
    private readonly IGenericRepository<CurrencyType> _currencyTypeRepository;
    private readonly IGenericRepository<RestaurantCurrencyType> _restaurantCurrencyTypeRepository;
    private readonly IMapper _mapper;

    public AddCurrencyTypesToRestaurantCommandHandler(
        IGenericRepository<CurrencyType> currencyTypeRepository,
        IGenericRepository<RestaurantCurrencyType> restaurantCurrencyTypeRepository,
        IMapper mapper)
    {
        _currencyTypeRepository = currencyTypeRepository;
        _restaurantCurrencyTypeRepository = restaurantCurrencyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CurrencyTypeResponse>>> Handle(
        AddCurrencyTypesToRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var currencyTypeIds = request.AddCurrencyTypeToRestaurantRequest.Ids;

        List<CurrencyType> currencyTypes = new();

        // Verify all currency types exist
        foreach (var currencyTypeId in currencyTypeIds)
        {
            var currencyType = await _currencyTypeRepository.GetByIdAsync(currencyTypeId);

            if (currencyType == null)
                return Result.Failure<List<CurrencyTypeResponse>>(DomainErrors.CurrencyType.NotFound(currencyTypeId));

            currencyTypes.Add(currencyType);
        }

        // Add new restaurant-currency type associations

        foreach (var currencyTypeId in currencyTypeIds)
        {
            var existingAssociation = await _restaurantCurrencyTypeRepository.GetFirstOrDefaultAsync(
                x => x.RestaurantId == restaurantId && x.CurrencyTypeId == currencyTypeId);

            if (existingAssociation == null)
            {
                var newAssociation = new RestaurantCurrencyType()
                {
                    Id = Guid.NewGuid(),
                    CurrencyTypeId = currencyTypeId,
                    RestaurantId = restaurantId
                };

                await _restaurantCurrencyTypeRepository.AddAsync(newAssociation);
                await _restaurantCurrencyTypeRepository.SaveChangesAsync();
            }
        }

        // Map to response DTOs
        var currencyTypeResponses = _mapper.Map<List<CurrencyTypeResponse>>(currencyTypes);

        return Result.Success(currencyTypeResponses);
    }
}