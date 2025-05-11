using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : ICommandHandler<DeleteRestaurantCommand, Restaurant?>
{
    private readonly IGenericRepository<Restaurant> restaurantRepository;

    public DeleteRestaurantCommandHandler(IGenericRepository<Restaurant> restaurantRepository)
    {
        this.restaurantRepository = restaurantRepository;
    }

    public async Task<Restaurant?> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.Id;

        var existingRestaurant = await restaurantRepository.GetByIdAsync(restaurantId);

        if(existingRestaurant == null) 
            return null;

        var restaurant = restaurantRepository.Remove(existingRestaurant);

        await restaurantRepository.SaveChangesAsync();

        return restaurant;
    }
}
