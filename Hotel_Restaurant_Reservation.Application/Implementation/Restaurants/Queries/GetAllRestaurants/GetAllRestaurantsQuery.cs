using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IQuery<Result<PagedResult<RestaurantResponse>>>
    {
        public GetAllRestaurantsQuery(
            int page = 1,
            int pageSize = 10,
            // --- C H A N G E: Added subName parameter ---
            string subName = null,
            Guid? tagId = null,
            Guid? featureId = null,
            Guid? cuisineId = null,
            Guid? dishId = null,
            Guid? mealTypeId = null,
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? localLocationId = null,
            double? minPrice = 0,
            double? maxPrice = double.MaxValue,
            double? minStarRating = 0,
            double? maxStarRating = 5)
        {
            Page = page;
            PageSize = pageSize;
            // --- C H A N G E: Assigned subName to the new property ---
            SubName = subName;

            TagId = tagId;
            FeatureId = featureId;
            CuisineId = cuisineId;
            DishId = dishId;
            MealTypeId = mealTypeId;
            CountryId = countryId;
            CityId = cityId;
            LocalLocationId = localLocationId;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            MinStarRating = minStarRating;
            MaxStarRating = maxStarRating;
        }

        // Pagination Properties
        public int Page { get; }
        public int PageSize { get; }
        public string SubName { get; }

        // Existing Filter Properties
        public Guid? TagId { get; }
        public Guid? FeatureId { get; }
        public Guid? CuisineId { get; }
        public Guid? DishId { get; }
        public Guid? MealTypeId { get; }
        public Guid? CountryId { get; }
        public Guid? CityId { get; }
        public Guid? LocalLocationId { get; }
        public double? MinPrice { get; }
        public double? MaxPrice { get; }
        public double? MinStarRating { get; }
        public double? MaxStarRating { get; }
    }
}
