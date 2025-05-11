using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandHandler : ICommandHandler<AddRestaurantCommand, Restaurant>
{
    private readonly IGenericRepository<Restaurant> restaurantRepository;
    private readonly IGenericRepository<Location> locationRepository;

    public AddRestaurantCommandHandler(IGenericRepository<Restaurant> restaurantRepository,
        IGenericRepository<Location> locationRepository)
    {
        this.restaurantRepository = restaurantRepository;
        this.locationRepository = locationRepository;
    }

    public async Task<Restaurant> Handle(AddRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = request.Restaurant;
        var location = request.Location;

        restaurant.Id = Guid.NewGuid();

        var existingLocation = await locationRepository.GetFirstOrDefaultAsync(x => x.CountryId == location.CountryId
        && x.CityLocalLocationsId == location.CityLocalLocationsId);

        if (existingLocation == null)
        {
            location.Id = Guid.NewGuid();
            location = await locationRepository.AddAsync(location);
            await locationRepository.SaveChangesAsync();
            restaurant.LocationId = location.Id;
        }
        else
        {
            restaurant.LocationId = existingLocation.Id;
        }


        restaurant = await restaurantRepository.AddAsync(restaurant);

        await restaurantRepository.SaveChangesAsync();

        return restaurant;
    }
}
