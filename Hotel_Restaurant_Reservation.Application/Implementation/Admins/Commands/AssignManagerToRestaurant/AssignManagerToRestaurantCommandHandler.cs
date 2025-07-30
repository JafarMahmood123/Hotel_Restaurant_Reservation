using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AssignManagerToRestaurant
{
    public class AssignManagerToRestaurantCommandHandler : ICommandHandler<AssignManagerToRestaurantCommand, Result>
    {
        private readonly IRestaurantRespository _restaurantRepository;
        private readonly IGenericRepository<User> _userRepository;

        public AssignManagerToRestaurantCommandHandler(IRestaurantRespository restaurantRepository, IGenericRepository<User> userRepository)
        {
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(AssignManagerToRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                return Result.Failure(DomainErrors.Restaurant.NotFound(request.RestaurantId));
            }

            await _restaurantRepository.UpdateAsync(request.RestaurantId, restaurant);
            await _restaurantRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}