using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : ICommandHandler<UpdateRestaurantCommand, Restaurant?>
{
    private readonly IGenericRepository<Restaurant> restaurantRepository;

    public UpdateRestaurantCommandHandler(IGenericRepository<Restaurant> restaurantRepository)
    {
        this.restaurantRepository = restaurantRepository;
    }

    public async Task<Restaurant?> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.Id;

        var newRestaurant = request.Restaurant;

        var updatedRestaurant = await restaurantRepository.UpdateAsync(restaurantId, newRestaurant);

        await restaurantRepository.SaveChangesAsync();

        if (updatedRestaurant == null) 
            return null;

        return updatedRestaurant;
    }
}
