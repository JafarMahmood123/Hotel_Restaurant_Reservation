using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.RemoveCuisineFromRestaurant;

public class RemoveCuisinesFromRestaurantCommandHandler : ICommandHandler<RemoveCuisinesFromRestaurantCommand, IEnumerable<Cuisine>>
{
    private readonly IGenericRepository<RestaurantCuisine> restaurantCuisineRepository;
    private readonly IGenericRepository<Cuisine> cuisineRepository;

    public RemoveCuisinesFromRestaurantCommandHandler(IGenericRepository<RestaurantCuisine> restaurantCuisineRepository,
        IGenericRepository<Cuisine> cuisineRepository)
    {
        this.restaurantCuisineRepository = restaurantCuisineRepository;
        this.cuisineRepository = cuisineRepository;
    }

    public async Task<IEnumerable<Cuisine>> Handle(RemoveCuisinesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;
        var cuisineIds = request.CuisineIds;


        List<RestaurantCuisine> restaurantCuisines = new List<RestaurantCuisine>();
        foreach (var cuisineId in cuisineIds)
        {
            var restaurantCuisine = await restaurantCuisineRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.CuisineId == cuisineId);

            restaurantCuisines.Add(restaurantCuisine);
        }

        restaurantCuisineRepository.RemoveRange(restaurantCuisines);

        await restaurantCuisineRepository.SaveChangesAsync();

        List<Cuisine> cuisines = new List<Cuisine>();

        foreach (var cuisineId in cuisineIds)
        {
            cuisines.Add(await cuisineRepository.GetByIdAsync(cuisineId));
        }

        return cuisines;
    }
}
