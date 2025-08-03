using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.UpdateRestaurantDish;

public class UpdateRestaurantDishCommandHandler : ICommandHandler<UpdateRestaurantDishCommand, Result>
{
    private readonly IGenericRepository<RestaurantDish> _restauranrDishRepository;
    private readonly IMapper _mapper;

    public UpdateRestaurantDishCommandHandler(IGenericRepository<RestaurantDish> restauranrDishRepository,
        IMapper mapper)
    {
        _restauranrDishRepository = restauranrDishRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateRestaurantDishCommand request, CancellationToken cancellationToken)
    {
        var restaurantDish = await _restauranrDishRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == request.RestaurantId
        && x.DishId == request.DishId);

        if(restaurantDish == null)
        {
            return Result.Failure(DomainErrors.Restaurant.DontHaveDish);
        }

        restaurantDish.Price = request.UpdateRestaurantDishRequest.Price;
        restaurantDish.Description = request.UpdateRestaurantDishRequest.Description;
        restaurantDish.PictureUrl = request.UpdateRestaurantDishRequest.PictureUrl;

        await _restauranrDishRepository.UpdateAsync(restaurantDish.Id, restaurantDish);
        await _restauranrDishRepository.SaveChangesAsync();

        return Result.Success();
    }
}
