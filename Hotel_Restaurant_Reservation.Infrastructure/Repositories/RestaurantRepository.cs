using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure; // Ensure this namespace is correct for your DbContext
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRespository
    {
        private readonly HotelRestaurantDbContext hotelRestaurantDbContext;

        public RestaurantRepository(HotelRestaurantDbContext hotelRestaurantDbContext)
        {
            this.hotelRestaurantDbContext = hotelRestaurantDbContext;
        }

        /// <summary>
        /// Returns the base IQueryable for the Restaurant entity.
        /// This is used for building more complex queries (e.g., with pagination)
        /// before executing them against the database.
        /// </summary>
        public IQueryable<Restaurant> GetAllQuery()
        {
            return hotelRestaurantDbContext.Restaurants.AsQueryable();
        }

        public IQueryable<Restaurant> GetFilteredRestaurantsQuery(Guid? tagId, Guid? featureId, Guid? cuisineId,
            Guid? dishId, Guid? mealTypeId, Guid? countryId, Guid? cityId, Guid? localLocationId, double? minPrice = 0,
            double? maxPrice = double.MaxValue, double? minStarRating = 0, double? maxStarRating = 5)
        {
            // Start with the base query including all related entities needed for filtering.
            IQueryable<Restaurant> restaurantsQuery = hotelRestaurantDbContext.Restaurants
                .Include(r => r.RestaurantTags)
                .Include(r => r.RestaurantFeatures)
                .Include(r => r.RestaurantCuisines)
                .Include(r => r.RestaurantDishPrices)
                .Include(r => r.RestaurantMealTypes)
                .Include(r => r.Location)
                    .ThenInclude(l => l.CityLocalLocations);

            // The .Include() calls inside the if blocks were redundant and incorrect, so they have been removed.
            // The filtering logic below works correctly with the Includes defined above.
            if (tagId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.RestaurantTags.Any(tag => tag.TagId == tagId));

            if (featureId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.RestaurantFeatures.Any(feature => feature.FeatureId == featureId));

            if (cuisineId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.RestaurantCuisines.Any(cuisine => cuisine.CuisineId == cuisineId));

            if (dishId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.RestaurantDishPrices.Any(dish => dish.DishId == dishId));

            if (mealTypeId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.RestaurantMealTypes.Any(mealType => mealType.MealTypeId == mealTypeId));

            if (countryId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.Location.CountryId == countryId);

            if (cityId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.Location.CityLocalLocations.CityId == cityId);

            if (localLocationId.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant =>
                    restaurant.Location.CityLocalLocations.LocalLocationId == localLocationId);



            if (minPrice.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.MinPrice >= minPrice);

            if (maxPrice.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.MaxPrice <= maxPrice);

            if (minStarRating.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.StarRating >= minStarRating);

            if (maxStarRating.HasValue)
                restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.StarRating <= maxStarRating);

            return restaurantsQuery;
        }

        #region Other Implemented Methods
        public async Task<Restaurant> AddAsync(Restaurant restaurant)
        {
            await hotelRestaurantDbContext.Restaurants.AddAsync(restaurant);
            return restaurant;
        }

        public async Task<IEnumerable<Restaurant>> AddRangeAsync(IEnumerable<Restaurant> entities)
        {
            await hotelRestaurantDbContext.Restaurants.AddRangeAsync(entities);
            return entities;
        }

        public IEnumerable<Restaurant> RemoveRange(IEnumerable<Restaurant> entities)
        {
            hotelRestaurantDbContext.Restaurants.RemoveRange(entities);
            return entities;
        }

        public async Task<IEnumerable<Restaurant>?> GetAllAsync()
        {
            return await hotelRestaurantDbContext.Restaurants.ToListAsync();
        }

        public Task<Restaurant?> GetByIdAsync(Guid id)
        {
            return hotelRestaurantDbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Restaurant?> GetFirstOrDefaultAsync(Expression<Func<Restaurant, bool>> predicate)
        {
            return await hotelRestaurantDbContext.Restaurants.FirstOrDefaultAsync(predicate);
        }

        public async Task<Restaurant?> RemoveAsync(Guid restaurantId)
        {
            var restaurant = await hotelRestaurantDbContext.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
                return null;
            hotelRestaurantDbContext.Remove(restaurant);
            return restaurant;
        }

        public async Task SaveChangesAsync()
        {
            await hotelRestaurantDbContext.SaveChangesAsync();
        }

        public async Task<Restaurant?> UpdateAsync(Guid id, Restaurant entity)
        {
            var existingRestaurant = await hotelRestaurantDbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRestaurant == null)
                return null;

            // This is a simple assignment; for a more robust update,
            // consider using AutoMapper or manually updating properties.
            existingRestaurant = entity;

            return existingRestaurant;
        }

        public IQueryable<Restaurant> Where(Expression<Func<Restaurant, bool>> predicate)
        {
            var filteredRestaurants = hotelRestaurantDbContext.Restaurants.Where(predicate);
            return filteredRestaurants;
        }

        public async Task<IEnumerable<Restaurant>> Take(int number)
        {
            return await hotelRestaurantDbContext.Restaurants.Take(number).ToListAsync();
        }
        #endregion
    }
}
