using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisinesToRestaurantCommandHandler : ICommandHandler<AddCuisinesToRestaurantCommand, Result<CuisineResponse>>
{
    private readonly IGenericRepository<Cuisine> _cuisineRepository;
    private readonly IGenericRepository<RestaurantCuisine> _restaurantCuisineRepository;
    private readonly IMapper _mapper;

    public AddCuisinesToRestaurantCommandHandler(IGenericRepository<Cuisine> cuisineRepository,
        IGenericRepository<RestaurantCuisine> restaurantCuisineRepository, IMapper mapper)
    {
        this._cuisineRepository = cuisineRepository;
        this._restaurantCuisineRepository = restaurantCuisineRepository;
        this._mapper = mapper;
    }

    public async Task<Result<CuisineResponse>> Handle(AddCuisinesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var cuisineId = request.CuisineId;

        var cuisine = await _cuisineRepository.GetByIdAsync(cuisineId);

        if (cuisine == null)
            return Result.Failure<CuisineResponse>(DomainErrors.Cuisine.NotExistCuisine(cuisineId));

        var restaurantCuisine = await _restaurantCuisineRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.CuisineId == cuisineId);

        if (restaurantCuisine != null)
        {
            return Result.Failure<CuisineResponse>(DomainErrors.Restaurant.AlreadyHaveCuisine);
        }

        restaurantCuisine = new RestaurantCuisine()
        {
            Id = Guid.NewGuid(),
            CuisineId = cuisineId,
            RestaurantId = restaurantId
        };

        await _restaurantCuisineRepository.AddAsync(restaurantCuisine);
        await _restaurantCuisineRepository.SaveChangesAsync();

        var cuisineResponses = _mapper.Map<CuisineResponse>(cuisine);

        return Result.Success(cuisineResponses);
    }

}
