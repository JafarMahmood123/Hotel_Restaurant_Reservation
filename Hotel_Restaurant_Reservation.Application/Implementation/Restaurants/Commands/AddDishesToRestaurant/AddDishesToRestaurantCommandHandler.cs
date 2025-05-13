using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddDishesToRestaurant;

public class AddDishesToRestaurantCommandHandler : ICommandHandler<AddDishesToRestaurantCommand, Dictionary<Dish, double>>
{
    private readonly IGenericRepository<Dish> dishRepository;
    private readonly IGenericRepository<RestaurantDishPrice> dishWithPriceRepository;

    public AddDishesToRestaurantCommandHandler(IGenericRepository<Dish> dishRepository,
        IGenericRepository<RestaurantDishPrice> dishWithPriceRepository)
    {
        this.dishRepository = dishRepository;
        this.dishWithPriceRepository = dishWithPriceRepository;
    }

    public async Task<Dictionary<Dish, double>> Handle(AddDishesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var dishIdsWithPrices = request.DishesIdsWithPrices;

        List<RestaurantDishPrice> newDishesWithPrices = new List<RestaurantDishPrice>();

        foreach (var dishIdWithPrice in dishIdsWithPrices)
        {
            var dishWithPrice = await dishWithPriceRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.DishId == dishIdWithPrice.Key);

            if (dishWithPrice == null)
            {

                dishWithPrice = new RestaurantDishPrice()
                {
                    Id = Guid.NewGuid(),
                    DishId = dishIdWithPrice.Key,
                    Price = dishIdWithPrice.Value,
                    RestaurantId = restaurantId
                };

                newDishesWithPrices.Add(dishWithPrice);
            }

        }

        await dishWithPriceRepository.AddRangeAsync(newDishesWithPrices);

        await dishWithPriceRepository.SaveChangesAsync();


        Dictionary<Dish, double> dishesWithPrices = new Dictionary<Dish, double>();

        foreach (var dishIdWithPrice in dishIdsWithPrices)
        {

            var dish = await dishRepository.GetByIdAsync(dishIdWithPrice.Key);

            var dishWithPrice = await dishWithPriceRepository.GetFirstOrDefaultAsync(x => x.DishId == dishIdWithPrice.Key
            && x.RestaurantId == restaurantId);

            dishesWithPrices.Add(dish, dishWithPrice.Price);
        }

        return dishesWithPrices;
    }
}
