using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Linq;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Repositories
{
    public interface IRestaurantRespository : IGenericRepository<Restaurant>
    {
        IQueryable<Restaurant> GetFilteredRestaurantsQuery(
            Guid? tagId,
            Guid? featureId,
            Guid? cuisineId,
            Guid? dishId,
            Guid? mealTypeId,
            Guid? countryId,
            Guid? cityId,
            Guid? localLocationId,
            double? minPrice = 0,
            double? maxPrice = double.MaxValue,
            double? minStarRating = 0,
            double? maxStarRating = 5);
    }
}
