using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCurrencyTypesToRestaurant;

public class AddCurrencyTypesToRestaurantCommandHandler : ICommandHandler<AddCurrencyTypesToRestaurantCommand, IEnumerable<CurrencyType>>
{
    private readonly IGenericRepository<CurrencyType> currencyTypeRepository;
    private readonly IGenericRepository<RestaurantCurrencyType> restaurantCurrencyTypeRepository;

    public AddCurrencyTypesToRestaurantCommandHandler(IGenericRepository<CurrencyType> currencyTypeRepository,
        IGenericRepository<RestaurantCurrencyType> restaurantCurrencyTypeRepository)
    {
        this.currencyTypeRepository = currencyTypeRepository;
        this.restaurantCurrencyTypeRepository = restaurantCurrencyTypeRepository;
    }

    public async Task<IEnumerable<CurrencyType>> Handle(AddCurrencyTypesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var currencyTypeIds = request.CurrencyTypeIds;

        List<RestaurantCurrencyType> restaurantCuisines = new List<RestaurantCurrencyType>();

        foreach (var currencyTypeId in currencyTypeIds)
        {
            var restaurantCuisine = await restaurantCurrencyTypeRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.CurrencyTypeId == currencyTypeId);

            if (restaurantCuisine == null)
            {

                restaurantCuisine = new RestaurantCurrencyType()
                {
                    Id = Guid.NewGuid(),
                    CurrencyTypeId = currencyTypeId,
                    RestaurantId = restaurantId
                };

                restaurantCuisines.Add(restaurantCuisine);
            }

        }

        await restaurantCurrencyTypeRepository.AddRangeAsync(restaurantCuisines);

        await restaurantCurrencyTypeRepository.SaveChangesAsync();


        List<CurrencyType> currencyTypes = new List<CurrencyType>();

        foreach (var cuisineId in currencyTypeIds)
        {
            currencyTypes.Add(await currencyTypeRepository.GetByIdAsync(cuisineId));
        }

        return currencyTypes;
    }
}
