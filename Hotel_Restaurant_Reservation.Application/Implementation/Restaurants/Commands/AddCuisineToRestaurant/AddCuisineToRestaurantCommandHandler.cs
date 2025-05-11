using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using MediatR;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisineToRestaurantCommandHandler : ICommandHandler<AddCuisineToRestaurantCommand, Cuisine>
{
    private readonly IGenericRepository<Cuisine> cuisineRepository;
    private readonly IGenericRepository<RestaurantCuisine> restaurantCuisineRepository;

    public AddCuisineToRestaurantCommandHandler(IGenericRepository<Cuisine> cuisineRepository,
        IGenericRepository<RestaurantCuisine> restaurantCuisineRepository)
    {
        this.cuisineRepository = cuisineRepository;
        this.restaurantCuisineRepository = restaurantCuisineRepository;
    }


    public async Task<Cuisine> Handle(AddCuisineToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var cuisineId = request.CuisineId;


        var existingRestaurantCuisine = await restaurantCuisineRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.CuisineId == cuisineId);


        if (existingRestaurantCuisine == null)
        {
            var newRestaurantCuisine = new RestaurantCuisine()
            {
                Id = Guid.NewGuid(),
                CuisineId = cuisineId,
                RestaurantId = restaurantId
            };

            var addedRestaurantCuisine = await restaurantCuisineRepository.AddAsync(newRestaurantCuisine);

            await restaurantCuisineRepository.SaveChangesAsync();
        }

        var cuisine = await cuisineRepository.GetByIdAsync(cuisineId);

        return cuisine;
    }
}
