using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddRestaurant;

public class AddRestaurantCommandHandler : ICommandHandler<AddRestaurantCommand, Restaurant>
{
    private readonly IGenericRepository<Restaurant> _genericRepository;

    public AddRestaurantCommandHandler(IGenericRepository<Restaurant> restaurantRepository)
    {
        _genericRepository = restaurantRepository;
    }

    public async Task<Restaurant> Handle(AddRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = request.Restaurant;
        var location = request.Location;

        restaurant.Id = Guid.NewGuid();
        restaurant.LocationId = location.Id;

        restaurant = await _genericRepository.AddAsync(restaurant);

        await _genericRepository.SaveChangesAsync();

        return restaurant;
    }
}
