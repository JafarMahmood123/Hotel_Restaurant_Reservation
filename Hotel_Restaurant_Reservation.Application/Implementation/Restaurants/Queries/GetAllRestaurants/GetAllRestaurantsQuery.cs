using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IQuery<IEnumerable<Restaurant>?>
{
    public GetAllRestaurantsQuery(Guid? tagId, Guid? featureId, Guid? cuisineId, Guid? dishId, Guid? mealTypeId,
        Guid? countryId, Guid? cityId, Guid? locationId, double? minPrice = 0, double? maxPrice = double.MaxValue,
        double? minStarRating = 0, double? maxStarRating = 5)
    {
        TagId = tagId;
        FeatureId = featureId;
        CuisineId = cuisineId;
        DishId = dishId;
        MealTypeId = mealTypeId;
        CountryId = countryId;
        CityId = cityId;
        LocationId = locationId;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        MinStarRating = minStarRating;
        MaxStarRating = maxStarRating;
    }

    public Guid? TagId { get; }
    public Guid? FeatureId { get; }
    public Guid? CuisineId { get; }
    public Guid? DishId { get; }
    public Guid? MealTypeId { get; }
    public Guid? CountryId { get; }
    public Guid? CityId { get; }
    public Guid? LocationId { get; }
    public double? MinPrice { get; }
    public double? MaxPrice { get; }
    public double? MinStarRating { get; }
    public double? MaxStarRating { get; }
}
