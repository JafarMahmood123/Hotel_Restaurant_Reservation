using AutoMapper;
using AutoMapper.Features;
using Azure;
using CsvHelper;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Seed.Fields;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Seed;

internal static class RestaurantSeeder
{
    public static string Path;

    private static HotelRestaurantDbContext hotelRestaurantDbContext = new DesignTimeDbContextFactory().
        CreateDbContext([]);

    public async static void Insert()
    {
        if (Path is null)
            throw new Exception("Path to the files is null.");

        using (var reader = new StreamReader(Path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<dynamic>();

            foreach (var record in records)
            {

                Restaurant restaurant = new Restaurant();

                Country country = new Country();
                City city = new City();
                Location location = new Location();
                LocalLocation localLocation = new LocalLocation();

                RestaurantRangePrices restaurantRangePrices = new RestaurantRangePrices();

                PriceLevel priceLevel = new PriceLevel();

                List<WorkTime> workTimes = new List<WorkTime>();

                List<Feature> features = new List<Feature>();

                List<Tag> tags = new List<Tag>();

                List<Cuisine> cuisines = new List<Cuisine>();

                List<Dish> dishes = new List<Dish>();

                List<MealType> mealsTypes = new List<MealType>();



                try
                {
                    await GenerateCountry(country);
                    GenerateCity(record, city);
                    GenerateLocalLocation(record, localLocation);
                    await GenerateLocation(country, city, location, localLocation);
                    GenerateRestaurant(record, restaurant, location);
                    GenerateRestaurantRangePrices(record, restaurant, restaurantRangePrices);
                    GenerateRestaurantPriceLevel(record, restaurant, priceLevel);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    GenerateWorkTimes(record, restaurant, workTimes, options);
                    GenerateFeatures(record, restaurant, features);
                    GenerateTags(record, restaurant, tags);
                    GenerateCuisines(record, restaurant, cuisines);
                    GenerateMealTypes(record, restaurant, mealsTypes);
                    GenerateDishes(record, restaurant, dishes);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }

    private static async Task GenerateDishes(dynamic record, Restaurant restaurant, List<Dish> dishes)
    {
        string dishesInJson = record.DISHS_WITH_PRICES;
        dishesInJson = dishesInJson.Replace("'", "\"");

        if (dishesInJson is null)
            throw new Exception("Work Times Is Null.");

        Dictionary<string, string> dishesFeilds =
            JsonSerializer.Deserialize<Dictionary<string, string>>(dishesInJson);


        List<KeyValuePair<string, string>> dishesWithPrices = dishesFeilds.ToList();

        foreach (KeyValuePair<string, string> item in dishesWithPrices)
        {
            string name = item.Key;
            double price = double.Parse(item.Value.Replace("$", ""));

            if (hotelRestaurantDbContext.Dishes.FirstOrDefault(x => x.Name == name && x.Price == price &&
            x.RestaurantId == restaurant.Id) is not null)
                return;

            Dish dish = new Dish()
            {
                Id = Guid.NewGuid(),
                Name = item.Key,
                Price = double.Parse(item.Value.Replace("$", "")),
                RestaurantId = restaurant.Id
            };

            //dishes.Add(dish);
            await hotelRestaurantDbContext.Dishes.AddAsync(dish);
        }
    }

    private static async Task GenerateMealTypes(dynamic record, Restaurant restaurant, List<MealType> mealsTypes)
    {
        string mealTypesInJson = record.MEAL_TYPES;
        mealTypesInJson = mealTypesInJson.Replace("'", "\"");

        if (mealTypesInJson is null)
            throw new Exception("Meal Types Is Null.");

        List<string> mealTypeStrings = JsonSerializer.Deserialize<List<string>>(mealTypesInJson);
        List<MealTypeFeild> meatTypeFeilds = mealTypeStrings.Select(f => new MealTypeFeild(f)).ToList();


        for (int i = 0; i < meatTypeFeilds.Count; i++)
        {
            string name = meatTypeFeilds[i].Name;
            if (hotelRestaurantDbContext.Features.FirstOrDefault(x => x.Name == name && x.RestaurantId == restaurant.Id) is not null)
                return;

            MealType mealType = new MealType()
            {
                Id = Guid.NewGuid(),
                Name = meatTypeFeilds[i].Name,
                RestaurantId = restaurant.Id
            };

            //mealsTypes.Append(mealType);
            await hotelRestaurantDbContext.MealTypes.AddAsync(mealType);
        }
    }

    private static async Task GenerateCuisines(dynamic record, Restaurant restaurant, List<Cuisine> cuisines)
    {
        string cuisinesInJson = record.CUISINES;
        cuisinesInJson = cuisinesInJson.Replace("'", "\"");

        if (cuisinesInJson is null)
            throw new Exception("Cuisines Is Null.");

        List<string> cuisineStrings = JsonSerializer.Deserialize<List<string>>(cuisinesInJson);
        List<CuisineFeild> cuisineFeilds = cuisineStrings.Select(f => new CuisineFeild(f)).ToList();


        for (int i = 0; i < cuisineFeilds.Count; i++)
        {
            string name = cuisineFeilds[i].Name;
            if (hotelRestaurantDbContext.Features.FirstOrDefault(x => x.Name == name && x.RestaurantId == restaurant.Id) is not null)
                return;

            Cuisine cuisine = new Cuisine()
            {
                Id = Guid.NewGuid(),
                Name = cuisineFeilds[i].Name,
                RestaurantId = restaurant.Id
            };

            //cuisines.Append(cuisine);
            await hotelRestaurantDbContext.Cuisines.AddAsync(cuisine);
        }
    }

    private static async Task GenerateTags(dynamic record, Restaurant restaurant, List<Tag> tags)
    {
        string tagsInJson = record.REVIEW_TAGS;
        tagsInJson = tagsInJson.Replace("'", "\"");

        if (tagsInJson is null)
            throw new Exception("Tags Is Null.");

        List<string> tagStrings = JsonSerializer.Deserialize<List<string>>(tagsInJson);
        List<TagFeild> tagFeilds = tagStrings.Select(f => new TagFeild(f)).ToList();


        for (int i = 0; i < tagFeilds.Count; i++)
        {
            string name = tagFeilds[i].Name;
            if (hotelRestaurantDbContext.Features.FirstOrDefault(x => x.Name == name && x.RestaurantId == restaurant.Id) is not null)
                return;

            Tag tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = tagFeilds[i].Name,
                RestaurantId = restaurant.Id
            };

            //tags.Append(tag);
            await hotelRestaurantDbContext.Tags.AddAsync(tag);
        }
    }

    private static async Task GenerateFeatures(dynamic record, Restaurant restaurant, List<Feature> features)
    {
        string FeaturesInJson = record.FEATURES;
        FeaturesInJson = FeaturesInJson.Replace("'", "\"");

        if (FeaturesInJson is null)
            throw new Exception("Features Is Null.");

        List<string> featureStrings = JsonSerializer.Deserialize<List<string>>(FeaturesInJson);
        List<FeatureFeild> featureFeilds = featureStrings.Select(f => new FeatureFeild(f)).ToList();


        for (int i = 0; i < featureFeilds.Count; i++)
        {
            string name = featureFeilds[i].Name;
            if (hotelRestaurantDbContext.Features.FirstOrDefault(x => x.Name == name && x.RestaurantId == restaurant.Id) is not null)
                return;

            Feature feature = new Feature()
            {
                Id = Guid.NewGuid(),
                Name = name,
                RestaurantId = restaurant.Id
            };

            //features.Append(feature);
            await hotelRestaurantDbContext.Features.AddAsync(feature);
        }
    }

    private static async Task GenerateWorkTimes(dynamic record, Restaurant restaurant, List<WorkTime> workTimes, JsonSerializerOptions options)
    {
        string workTimesInJson = record.HOURS;
        workTimesInJson = workTimesInJson.Replace("'", "\"");

        if (workTimesInJson is null)
            throw new Exception("Work Times Is Null.");

        List<List<WorkTimeField>> workTimeFeilds =
            JsonSerializer.Deserialize<List<List<WorkTimeField>>>(workTimesInJson, options);


        for (int i = 0; i < workTimeFeilds.Count; i++)
        {
            foreach (WorkTimeField workTimeField in workTimeFeilds[i])
            {
                TimeOnly openHour = TimeOnly.Parse(workTimeField.OpenHours);
                TimeOnly closeHour = TimeOnly.Parse(workTimeField.CloseHours);
                DayOfWeek day = (DayOfWeek)i;

                if (hotelRestaurantDbContext.WorkTimes.FirstOrDefault(x => x.Day == day && x.OpenHour == openHour
                && x.CloseHour == closeHour && x.RestaurantId == restaurant.Id) is not null)
                    return;

                WorkTime workTime = new WorkTime()
                {
                    Id = Guid.NewGuid(),
                    Day = day,
                    OpenHour = openHour,
                    CloseHour = closeHour,
                    RestaurantId = restaurant.Id
                };

                //workTimes.Append(workTime);
                await hotelRestaurantDbContext.WorkTimes.AddAsync(workTime);
            }
        }
    }

    private static async Task GenerateRestaurantPriceLevel(dynamic record, Restaurant restaurant, PriceLevel priceLevel)
    {
        if (hotelRestaurantDbContext.PriceLevels.FirstOrDefault(x => x.RestaurantId == restaurant.Id) is not null)
            return;

        priceLevel.Id = Guid.NewGuid();
        priceLevel.Level = record.PRICE_LEVEL;
        priceLevel.RestaurantId = restaurant.Id;

        await hotelRestaurantDbContext.PriceLevels.AddAsync(priceLevel);
    }

    private static async Task GenerateRestaurantRangePrices(dynamic record, Restaurant restaurant, RestaurantRangePrices restaurantRangePrices)
    {
        if (hotelRestaurantDbContext.RestaurantRangePrices.FirstOrDefault(x => x.RestaurantId == restaurant.Id) is not null)
            return;

        restaurantRangePrices.Id = Guid.NewGuid();
        restaurantRangePrices.MinPrice = Double.Parse(record.MIN_PRICE);
        restaurantRangePrices.MaxPrice = Double.Parse(record.MAX_PRICE);
        restaurantRangePrices.RestaurantId = restaurant.Id;

        await hotelRestaurantDbContext.RestaurantRangePrices.AddAsync(restaurantRangePrices);
    }

    private static async Task GenerateRestaurant(dynamic record, Restaurant restaurant, Location location)
    {
        string name = record.NAME;
        string url = record.RESTAURANT_URL;
        string pictureUrl = record.PICTURE;
        double starRating = Double.Parse(record.RATING);
        string description = record.DESCRIPTION;
        double latitude = Double.Parse(record.LATITUDE);
        double longitude = Double.Parse(record.LONGITUDE);
        int numberOfTables = int.Parse(record.TABLES_NUMBER);
        Guid locationId = location.Id;

        if (hotelRestaurantDbContext.Restaurants.FirstOrDefault(x => x.Name == name && x.Url == url
        && x.PictureUrl == pictureUrl && x.StarRating == starRating && x.Description == description
        && x.Latitude == latitude && x.Longitude == longitude
        && x.NumberOfTables == numberOfTables && x.LocationId == location.Id) is not null)
            return;

        restaurant.Id = Guid.NewGuid();
        restaurant.Name = name;
        restaurant.Url = url;
        restaurant.PictureUrl = pictureUrl;
        restaurant.StarRating = starRating;
        restaurant.Description = description;
        restaurant.Latitude = latitude;
        restaurant.Longitude = longitude;
        restaurant.NumberOfTables = numberOfTables;
        restaurant.LocationId = locationId;

        await hotelRestaurantDbContext.Restaurants.AddAsync(restaurant);
    }

    private static async Task GenerateLocation(Country country, City city, Location location, LocalLocation localLocation)
    {
        if(hotelRestaurantDbContext.Locations.FirstOrDefault(x=>x.CountryId==country.Id
        && x.CityId==city.Id && x.LocalLocationId==localLocation.Id) is not null)
            return;

        location.Id = Guid.NewGuid();
        location.CountryId = country.Id;
        location.CityId = city.Id;
        location.LocalLocationId = localLocation.Id;

        await hotelRestaurantDbContext.AddAsync(location);
    }

    private static async Task GenerateLocalLocation(dynamic record, LocalLocation localLocation)
    {
        string name = record.GENERAL_LOCATION;
        if (hotelRestaurantDbContext.LocalLocations.FirstOrDefault(x => x.Name == name) is not null)
            return;


        localLocation.Id = Guid.NewGuid();
        localLocation.Name = record.GENERAL_LOCATION;

        await hotelRestaurantDbContext.AddAsync(localLocation);
    }

    private static async Task GenerateCity(dynamic record, City city)
    {
        string name = record.DESTINATION;

        if (hotelRestaurantDbContext.Cities.FirstOrDefault(x => x.Name == name) is not null)
            return;

        city.Id = Guid.NewGuid();
        city.Name = record.DESTINATION;

        await hotelRestaurantDbContext.Cities.AddAsync(city);
    }

    private static async Task GenerateCountry(Country country)
    {
        string countryName = string.Empty;

        if (hotelRestaurantDbContext.Countries.FirstOrDefault(x => x.Name == countryName) is not null)
            return;

        country.Id = Guid.NewGuid();
        country.Name = string.Empty;

        await hotelRestaurantDbContext.Countries.AddAsync(country);
    }
}
