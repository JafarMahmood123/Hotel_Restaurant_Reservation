using Hotel_Restaurant_Reservation.Domain.Abstractions;
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

    public Restaurant Add(Restaurant restaurant)
    {
        hotelRestaurantDbContext.Restaurants.Add(restaurant);
        return restaurant;
    }

    public async Task<Restaurant> AddAsync(Restaurant restaurant)
    {
        await hotelRestaurantDbContext.Restaurants.AddAsync(restaurant);
        return restaurant;
    }

    public IEnumerable<Restaurant>? GetAll()
    {
        return hotelRestaurantDbContext.Restaurants.ToList();
    }

    public async Task<IEnumerable<Restaurant>?> GetAllAsync()
    {
        return await hotelRestaurantDbContext.Restaurants.ToListAsync();
    }

    public Restaurant? GetById(Guid id)
    {
        return hotelRestaurantDbContext.Restaurants.FirstOrDefault(x => x.Id == id);
    }

    public Task<Restaurant?> GetByIdAsync(Guid id)
    {
        return hotelRestaurantDbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IEnumerable<Restaurant>? GetFilteredRestaurants(Guid? tagId, Guid? featureId, Guid? cuisineId, Guid? dishId,
        Guid? countryId, Guid? cityId, Guid? locationId, Guid? mealTypeId, double? minPrice = 0,
        double? maxPrice = double.MaxValue, double? minStarRating = 0, double? maxStarRating = 5)
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
            restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.Location.CountryId == countryId);

        if (cityId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.Location.CityId == cityId);

        if (locationId is not null)
            restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.Location.LocalLocationId == locationId);

        restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.MinPrice >= minPrice && restaurant.MaxPrice <= maxPrice);

        restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.StarRating >= minStarRating && restaurant.StarRating <= maxStarRating);


        return restaurantsQuery.ToList();
    }

    public async Task<IEnumerable<Restaurant>?> GetFilteredRestaurantsAsync(Guid? tagId, Guid? featureId, Guid? cuisineId,
        Guid? dishId, Guid? mealTypeId, Guid? countryId, Guid? cityId, Guid? locationId,
        double? minPrice = 0, double? maxPrice = double.MaxValue, double? minStarRating = 0,
        double? maxStarRating = 5)
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

        restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.MinPrice >= minPrice && restaurant.MaxPrice <= maxPrice);

        restaurantsQuery = restaurantsQuery.Where(restaurant => restaurant.StarRating >= minStarRating && restaurant.StarRating <= maxStarRating);


        return await restaurantsQuery.ToListAsync();
    }

    public async Task<Restaurant?> GetFirstOrDefaultAsync(Expression<Func<Restaurant, bool>> predicate)
    {
        return await hotelRestaurantDbContext.Restaurants.FirstOrDefaultAsync(predicate);
    }

    public Restaurant Remove(Restaurant restaurant)
    {
        hotelRestaurantDbContext.Remove(restaurant);
        return restaurant;
    }

    public async Task SaveChangesAsync()
    {
        await hotelRestaurantDbContext.SaveChangesAsync();
    }

    public Restaurant? Update(Guid id, Restaurant restaurant)
    {
        var existingRestaurant = hotelRestaurantDbContext.Restaurants.FirstOrDefault(x => x.Id == id);
        if (existingRestaurant == null)
            return null;

        existingRestaurant = restaurant;

        return existingRestaurant;
    }

    public async Task<Restaurant?> UpdateAsync(Guid id, Restaurant entity)
    {
        var existingRestaurant = await hotelRestaurantDbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRestaurant == null)
            return null;

        existingRestaurant = entity;

        return existingRestaurant;
    }
}
