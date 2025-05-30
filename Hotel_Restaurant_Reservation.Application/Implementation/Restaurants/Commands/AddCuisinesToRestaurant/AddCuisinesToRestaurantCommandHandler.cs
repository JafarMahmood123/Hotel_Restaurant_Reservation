using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Commands.AddCuisinesToRestaurant;

public class AddCuisinesToRestaurantCommandHandler : ICommandHandler<AddCuisinesToRestaurantCommand, Result<List<CuisineResponse>>>
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

    public async Task<Result<List<CuisineResponse>>> Handle(AddCuisinesToRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.RestaurantId;

        var cuisineIds = request.AddCuisineToRestaurantRequest.Ids;

        List<Cuisine> cuisines = new List<Cuisine>();

        foreach (var cuisineId in cuisineIds)
        {
            var cuisine = await _cuisineRepository.GetByIdAsync(cuisineId);

            if (cuisine == null)
                return Result.Failure<List<CuisineResponse>>(DomainErrors.Cuisine.NotExistCuisine(cuisineId));

            cuisines.Add(cuisine);
        }

        foreach (var cuisineId in cuisineIds)
        {
            var restaurantCuisine = await _restaurantCuisineRepository.GetFirstOrDefaultAsync(x => x.RestaurantId == restaurantId
            && x.CuisineId == cuisineId);

            if (restaurantCuisine == null)
            {
                restaurantCuisine = new RestaurantCuisine()
                {
                    Id = Guid.NewGuid(),
                    CuisineId = cuisineId,
                    RestaurantId = restaurantId
                };

                await _restaurantCuisineRepository.AddAsync(restaurantCuisine);
                await _restaurantCuisineRepository.SaveChangesAsync();
            }
            
        }

        var cuisineResponses = _mapper.Map<List<CuisineResponse>>(cuisines);

        return Result.Success(cuisineResponses);
    }

}
