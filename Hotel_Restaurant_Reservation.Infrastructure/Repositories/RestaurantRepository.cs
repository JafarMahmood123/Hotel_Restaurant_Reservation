using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRespository
{
    private readonly HotelRestaurantDbContext hotelRestaurantDbContext;

    public RestaurantRepository(HotelRestaurantDbContext hotelRestaurantDbContext)
    {
        this.hotelRestaurantDbContext = hotelRestaurantDbContext;
    }

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

    public async Task<IEnumerable<Restaurant>?> GetFilteredRestaurantsAsync(Guid? tagId, Guid? featureId, Guid? cuisineId,
        Guid? dishId, Guid? mealTypeId, Guid? countryId, Guid? cityId, Guid? localLocationId, double? minPrice = 0,
        double? maxPrice = double.MaxValue, double? minStarRating = 0,double? maxStarRating = 5)
    {
        IQueryable<Restaurant> restaurantsQuery = hotelRestaurantDbContext.Restaurants;

        if (tagId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.RestaurantTags.Where(tag => tag.TagId == tagId).Any());

        if (featureId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.RestaurantFeatures.Where(feature => feature.FeatureId == featureId).Any());

        if (cuisineId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.RestaurantCuisines.Where(cuisine => cuisine.CuisineId == cuisineId).Any());

        if (dishId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.RestaurantDishPrices.Where(dish => dish.DishId == dishId).Any());

        if (mealTypeId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.RestaurantMealTypes.Where(mealType => mealType.MealTypeId == mealTypeId).Any());

        if (countryId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.Location.CountryId == countryId);

        if (cityId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.Location.CityLocalLocations.CityId == cityId);

        if(localLocationId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant =>
            restaurant.Location.CityLocalLocations.LocalLocationId == localLocationId);


        restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.MinPrice >= minPrice && restaurant.MaxPrice <= maxPrice);

        restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.StarRating >= minStarRating && restaurant.StarRating <= maxStarRating);


        return await restaurantsQuery.ToListAsync();
    }

    public async Task<Restaurant?> GetFirstOrDefaultAsync(Expression<Func<Restaurant, bool>> predicate)
    {
        return await hotelRestaurantDbContext.Restaurants.FirstOrDefaultAsync(predicate);
    }

    public async Task<Restaurant?> RemoveAsync(Guid restaurantId)
    {
        var restaurant = await hotelRestaurantDbContext.Restaurants.FindAsync(restaurantId);

        if(restaurant == null)
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

        existingRestaurant = entity;

        return existingRestaurant;
    }

    public IQueryable<Restaurant> Where(Expression<Func<Restaurant, bool>> predicate)
    {
        var filteredRestaurants = hotelRestaurantDbContext.Restaurants.Where(predicate);
        return filteredRestaurants;
    }
}
