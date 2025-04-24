using CsvHelper;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Seed.Fields;
using System.Globalization;
using System.Text.Json;

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



                try
                {
                    await GenerateCountry(country);
                    GenerateCity(record, city);
                    GenerateLocalLocation(record, localLocation);
                    await GenerateLocation(country, city, location, localLocation);
                    GenerateRestaurant(record, restaurant, location);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    GenerateWorkTimes(record, restaurant, options);
                    GenerateFeatures(record, restaurant);
                    GenerateTags(record, restaurant);
                    GenerateCuisines(record, restaurant);
                    GenerateMealTypes(record, restaurant);
                    GenerateDishes(record, restaurant);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }

    private static async Task GenerateDishes(dynamic record, Restaurant restaurant)
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
            Dish dish = new Dish()
            {
                Id = Guid.NewGuid(),
                Name = item.Key,
            };

            if (hotelRestaurantDbContext.Dishes.FirstOrDefault(x => x.Name == name) is null)
                await hotelRestaurantDbContext.Dishes.AddAsync(dish);


            else
            {
                dish = hotelRestaurantDbContext.Dishes.FirstOrDefault(dish => dish.Name == name);
            }

            RestaurantDishPrice restaurantDishPrice = new RestaurantDishPrice()
            {
                Price = price,
                RestaurantId = restaurant.Id,
                DishId = dish.Id
            };


            await hotelRestaurantDbContext.RestaurantDishPrices.AddAsync(restaurantDishPrice);
        }
    }

    private static async Task GenerateMealTypes(dynamic record, Restaurant restaurant)
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

            MealType mealType = new MealType()
            {
                Id = Guid.NewGuid(),
                Name = meatTypeFeilds[i].Name,
            };

            if (hotelRestaurantDbContext.MealTypes.FirstOrDefault(x => x.Name == name) is null)
                await hotelRestaurantDbContext.MealTypes.AddAsync(mealType);

            else
            {
                mealType = hotelRestaurantDbContext.MealTypes.FirstOrDefault(x => x.Name == name);
            }

            RestaurantMealType restaurantMealType = new RestaurantMealType()
            {
                Id= Guid.NewGuid(),
                MealTypeId = mealType.Id,
                RestaurantId = restaurant.Id
            };

            await hotelRestaurantDbContext.RestaurantMealTypes.AddAsync(restaurantMealType);
        }
    }

    private static async Task GenerateCuisines(dynamic record, Restaurant restaurant)
    {
        string cuisinesInJson = record.CUISINES;
        cuisinesInJson = cuisinesInJson.Replace("'", "\"");

        if (cuisinesInJson is null)
            throw new Exception("Cuisines Is Null.");

        List<string> cuisineStrings = JsonSerializer.Deserialize<List<string>>(cuisinesInJson);
        List<CuisineFeild> cuisineFeilds = cuisineStrings.Select(f => new CuisineFeild(f)).ToList();


        for (int i = 0; i < cuisineFeilds.Count; i++)
        {
            Cuisine cuisine = new Cuisine()
            {
                Id = Guid.NewGuid(),
                Name = cuisineFeilds[i].Name,
            };

            string name = cuisineFeilds[i].Name;
            if (hotelRestaurantDbContext.Cuisines.FirstOrDefault(x => x.Name == name) is null)
                await hotelRestaurantDbContext.AddAsync(cuisine);
            else
            {
                cuisine = hotelRestaurantDbContext.Cuisines.FirstOrDefault(x => x.Name == name);
            }

            RestaurantCuisine restaurantCuisine = new RestaurantCuisine()
            {
                Id = Guid.NewGuid(),
                CuisineId = cuisine.Id,
                RestaurantId = restaurant.Id
            };

            await hotelRestaurantDbContext.RestaurantCuisines.AddAsync(restaurantCuisine);
        }
    }

    private static async Task GenerateTags(dynamic record, Restaurant restaurant)
    {
        string tagsInJson = record.REVIEW_TAGS;
        tagsInJson = tagsInJson.Replace("'", "\"");

        if (tagsInJson is null)
            throw new Exception("Tags Is Null.");

        List<string> tagStrings = JsonSerializer.Deserialize<List<string>>(tagsInJson);
        List<TagFeild> tagFeilds = tagStrings.Select(f => new TagFeild(f)).ToList();


        for (int i = 0; i < tagFeilds.Count; i++)
        {
            Tag tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Name = tagFeilds[i].Name,
            };

            string name = tagFeilds[i].Name;
            if (hotelRestaurantDbContext.Tags.FirstOrDefault(x => x.Name == name) is null)
                await hotelRestaurantDbContext.Tags.AddAsync(tag);

            else
            {
                tag = hotelRestaurantDbContext.Tags.FirstOrDefault(tag => tag.Name == name);
            }

            RestaurantTag restaurantTag = new RestaurantTag()
            {
                Id = Guid.NewGuid(),
                TagId = tag.Id,
                RestaurantId = restaurant.Id
            };

            await hotelRestaurantDbContext.RestaurantTags.AddAsync(restaurantTag);
        }
    }

    private static async Task GenerateFeatures(dynamic record, Restaurant restaurant)
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
            Feature feature = new Feature()
            {
                Id = Guid.NewGuid(),
                Name = name,
            };

            if (hotelRestaurantDbContext.Features.FirstOrDefault(x => x.Name == name) is null)
                await hotelRestaurantDbContext.Features.AddAsync(feature);

            else
            {
                feature = hotelRestaurantDbContext.Features.FirstOrDefault(feature => feature.Name == name);
            }

            RestaurantFeature restaurantFeature = new RestaurantFeature()
            {
                Id = Guid.NewGuid(),
                FeatureId = feature.Id,
                RestaurantId = restaurant.Id
            };

            await hotelRestaurantDbContext.RestaurantFeatures.AddAsync(restaurantFeature);
        }
    }

    private static async Task GenerateWorkTimes(dynamic record, Restaurant restaurant, JsonSerializerOptions options)
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

                WorkTime workTime = new WorkTime()
                {
                    Id = Guid.NewGuid(),
                    Day = day,
                    OpenHour = openHour,
                    CloseHour = closeHour,
                };

                if (hotelRestaurantDbContext.WorkTimes.FirstOrDefault(x => x.Day == day && x.OpenHour == openHour
                && x.CloseHour == closeHour) is null)
                    await hotelRestaurantDbContext.WorkTimes.AddAsync(workTime);

                else
                {
                    workTime = hotelRestaurantDbContext.WorkTimes.FirstOrDefault(x => x.Day == day && x.OpenHour == openHour
                    && x.CloseHour == closeHour);
                }

                RestaurantWorkTime restaurantWorkTime = new RestaurantWorkTime()
                {
                    Id = Guid.NewGuid(),
                    WorkTimeId = workTime.Id,
                    RestaurantId = restaurant.Id
                };

                await hotelRestaurantDbContext.RestaurantWorkTimes.AddAsync(restaurantWorkTime);
            }
        }
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
        string priceLevel = record.PRICE_LEVEL;
        double minPrice = Double.Parse(record.MIN_PRICE);
        double maxPrice = Double.Parse(record.MAX_PRICE);
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
        restaurant.PriceLevel = priceLevel;
        restaurant.MinPrice = minPrice;
        restaurant.MaxPrice = maxPrice;
        restaurant.LocationId = locationId;

        await hotelRestaurantDbContext.Restaurants.AddAsync(restaurant);
    }

    private static async Task GenerateLocation(Country country, City city, Location location, LocalLocation localLocation)
    {
        if (hotelRestaurantDbContext.Locations.FirstOrDefault(x => x.CountryId == country.Id
        && x.CityId == city.Id && x.LocalLocationId == localLocation.Id) is not null)
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
