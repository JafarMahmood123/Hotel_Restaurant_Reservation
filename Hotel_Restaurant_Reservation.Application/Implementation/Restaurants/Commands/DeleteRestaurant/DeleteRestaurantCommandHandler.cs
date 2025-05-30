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

        var restaurant = await restaurantRepository.RemoveAsync(restaurantId);

        if (restaurant == null)
            return null;

        await restaurantRepository.SaveChangesAsync();

        return restaurant;
    }
}
