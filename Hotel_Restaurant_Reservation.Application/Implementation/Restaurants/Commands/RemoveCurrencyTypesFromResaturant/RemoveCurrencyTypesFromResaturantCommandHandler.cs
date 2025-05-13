using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisineFromRestaurant;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCurrencyTypesFromResaturant;

public class RemoveCurrencyTypesFromResaturantCommandHandler : ICommandHandler<RemoveCurrencyTypesFromResaturantCommand, IEnumerable<CurrencyType>>
{
    private readonly IGenericRepository<RestaurantCurrencyType> restaurantCurrenctTypeRepository;
    private readonly IGenericRepository<CurrencyType> currencyTypeRepository;

    public RemoveCurrencyTypesFromResaturantCommandHandler(IGenericRepository<RestaurantCurrencyType> restaurantCurrenctTypeRepository,
        IGenericRepository<CurrencyType> currencyTypeRepository)
    {
        this.restaurantCurrenctTypeRepository = restaurantCurrenctTypeRepository;
        this.currencyTypeRepository = currencyTypeRepository;
    }

    public async Task<IEnumerable<CurrencyType>> Handle(RemoveCurrencyTypesFromResaturantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var currencyTypeIds = request.CurrencyTypeIds;


        List<RestaurantCurrencyType> restaurantCurrencyTypes = new List<RestaurantCurrencyType>();
        foreach (var currencyTypeId in currencyTypeIds)
        {
            var restaurantCurrencyType = await restaurantCurrenctTypeRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.CurrencyTypeId == currencyTypeId);

            restaurantCurrencyTypes.Add(restaurantCurrencyType);
        }

        restaurantCurrenctTypeRepository.RemoveRange(restaurantCurrencyTypes);

        await restaurantCurrenctTypeRepository.SaveChangesAsync();

        List<CurrencyType> currencyTypes = new List<CurrencyType>();

        foreach (var currencyTypeId in currencyTypeIds)
        {
            currencyTypes.Add(await currencyTypeRepository.GetByIdAsync(currencyTypeId));
        }

        return currencyTypes;
    }
}
