using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using MediatR;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisinesToRestaurantCommandHandler : ICommandHandler<AddCuisinesToRestaurantCommand, IEnumerable<Cuisine>>
{
    private readonly IGenericRepository<Cuisine> cuisineRepository;
    private readonly IGenericRepository<RestaurantCuisine> restaurantCuisineRepository;

    public AddCuisinesToRestaurantCommandHandler(IGenericRepository<Cuisine> cuisineRepository,
        IGenericRepository<RestaurantCuisine> restaurantCuisineRepository)
    {
        this.cuisineRepository = cuisineRepository;
        this.restaurantCuisineRepository = restaurantCuisineRepository;
    }

    public async Task<IEnumerable<Cuisine>> Handle(AddCuisinesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var cuisineIds = request.CuisineIds;

        List<RestaurantCuisine> restaurantCuisines = new List<RestaurantCuisine>();

        foreach (var cuisineId in cuisineIds)
        {
            var restaurantCuisine = await restaurantCuisineRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.CuisineId == cuisineId);

            if (restaurantCuisine == null)
            {

                restaurantCuisine = new RestaurantCuisine()
                {
                    Id = Guid.NewGuid(),
                    CuisineId = cuisineId,
                    RestaurantId = restaurantId
                };

                restaurantCuisines.Add(restaurantCuisine);
            }
            
        }

        await restaurantCuisineRepository.AddRangeAsync(restaurantCuisines);

        await restaurantCuisineRepository.SaveChangesAsync();


        List<Cuisine> cuisines = new List<Cuisine>();

        foreach (var cuisineId in cuisineIds)
        {
            cuisines.Add(await cuisineRepository.GetByIdAsync(cuisineId));
        }

        return cuisines;
    }

}
