using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveDishesFromRestaurant;

public class RemoveDishesFromRestaurantCommandHandler : ICommandHandler<RemoveDishesFromRestaurantCommand, Dictionary<Dish, double>>
{
    private readonly IGenericRepository<RestaurantDishPrice> restaurantDishPriceRepository;
    private readonly IGenericRepository<Dish> dishRepository;

    public RemoveDishesFromRestaurantCommandHandler(IGenericRepository<RestaurantDishPrice> restaurantDishPriceRepository,
        IGenericRepository<Dish> dishRepository)
    {
        this.restaurantDishPriceRepository = restaurantDishPriceRepository;
        this.dishRepository = dishRepository;
    }

    public async Task<Dictionary<Dish, double>> Handle(RemoveDishesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var dishIds = request.DishesIds;


        List<RestaurantDishPrice> restaurantDishesWithPrices = new List<RestaurantDishPrice>();
        foreach (var dishId in dishIds)
        {
            var restaurantDishPrice = await restaurantDishPriceRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.DishId == dishId);

            restaurantDishesWithPrices.Add(restaurantDishPrice);
        }

        restaurantDishPriceRepository.RemoveRange(restaurantDishesWithPrices);

        await restaurantDishPriceRepository.SaveChangesAsync();

        Dictionary<Dish, double> dishesWithPrices = new Dictionary<Dish, double>();

        foreach (var dishWithPrice in restaurantDishesWithPrices)
        {
            var dish = await dishRepository.GetByIdAsync(dishWithPrice.DishId);

            dishesWithPrices.Add(dish, dishWithPrice.Price);
        }

        return dishesWithPrices;
    }
}
