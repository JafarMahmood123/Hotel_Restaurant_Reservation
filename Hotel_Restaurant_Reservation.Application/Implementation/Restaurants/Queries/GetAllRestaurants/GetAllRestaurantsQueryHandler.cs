using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler : IQueryHandler<GetAllRestaurantsQuery, IEnumerable<Restaurant>?>
{
    private readonly IRestaurantRespository restaurantRespository;

    public GetAllRestaurantsQueryHandler(IRestaurantRespository restaurantRespository)
    {
        this.restaurantRespository = restaurantRespository;
    }

    public async Task<IEnumerable<Restaurant>?> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        return await restaurantRespository.GetFilteredRestaurantsAsync(request.TagId, request.FeatureId,
            request.CuisineId, request.DishId, request.MealTypeId, request.CountryId, request.CityId, request.LocalLocationId,
            request.MinPrice, request.MaxPrice, request.MinStarRating, request.MaxStarRating);
    }
}
