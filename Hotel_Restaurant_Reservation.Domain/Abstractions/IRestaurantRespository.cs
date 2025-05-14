using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Domain.Abstractions;

public interface IRestaurantRespository : IGenericRepository<Restaurant>
{
    public IEnumerable<Restaurant>? GetFilteredRestaurants(Guid? tagId, Guid? featureId, Guid? cuisineId,
        Guid? dishId, Guid? mealTypeId, Guid? countryId, Guid? cityLocalLocationId, double? minPrice = 0,
        double? maxPrice = double.MaxValue, double? minStarRating = 0, double? maxStarRating = 5);

    public Task<IEnumerable<Restaurant>?> GetFilteredRestaurantsAsync(Guid? tagId, Guid? featureId, Guid? cuisineId,
        Guid? dishId, Guid? mealTypeId, Guid? countryId, Guid? cityId, Guid? localLocationId, double? minPrice = 0,
        double? maxPrice = double.MaxValue, double? minStarRating = 0, double? maxStarRating = 5);
}
