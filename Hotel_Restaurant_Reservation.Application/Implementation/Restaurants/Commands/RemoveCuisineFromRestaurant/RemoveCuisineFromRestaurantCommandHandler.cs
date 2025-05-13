using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisineFromRestaurant;

public class RemoveCuisineFromRestaurantCommandHandler : ICommandHandler<RemoveCuisineFromRestaurantCommand, Cuisine>
{
    private readonly IGenericRepository<RestaurantCuisine> restaurantCuisineRepository;
    private readonly IGenericRepository<Cuisine> cuisineRepository;

    public RemoveCuisineFromRestaurantCommandHandler(IGenericRepository<RestaurantCuisine> restaurantCuisineRepository,
        IGenericRepository<Cuisine> cuisineRepository)
    {
        this.restaurantCuisineRepository = restaurantCuisineRepository;
        this.cuisineRepository = cuisineRepository;
    }

    public async Task<Cuisine> Handle(RemoveCuisineFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var cuisineId = request.CuisineId;

        var restaurantCuisine = await restaurantCuisineRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
        && x.CuisineId == cuisineId);

        restaurantCuisineRepository.Remove(restaurantCuisine);

        await restaurantCuisineRepository.SaveChangesAsync();

        var cuisine = await cuisineRepository.GetByIdAsync(cuisineId);

        return cuisine;
    }
}
