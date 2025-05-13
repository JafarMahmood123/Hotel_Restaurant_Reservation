using CsvHelper;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Seed.Fields;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text.Json;

namespace Hotel_Restaurant_Reservation.Seed;

internal static class RestaurantSeeder
{
    public static string Path;

    private static HotelRestaurantDbContext hotelRestaurantDbContext = new DesignTimeDbContextFactory().
        CreateDbContext([]);

    private static int recordNumber = 0;

    private static int NumberOfErrors = 0;
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
                recordNumber++;

                Restaurant restaurant = new Restaurant();

                Country country = new Country();
                City city = new City();
                Location location = new Location();
                LocalLocation localLocation = new LocalLocation();

                CityLocalLocations cityLocalLocations = new CityLocalLocations();

                CurrencyType currencyType = new CurrencyType();



                try
                {
                    GenerateCountry(country);
                    GenerateCity(record, city, country);
                    GenerateLocalLocation(record, localLocation, city, cityLocalLocations);
                    GenerateLocation(country, cityLocalLocations, location);
                    GenerateRestaurant(record, restaurant, location);
                    GenerateRestaurantCurrencyType(restaurant,currencyType);

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
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    NumberOfErrors++;
                    Console.WriteLine(ex.Message);
                }
            }
        }

        Console.WriteLine("The number of total errors = " + NumberOfErrors);
    }

    private static void GenerateRestaurantCurrencyType(Restaurant restaurant, CurrencyType currencyType)
    {
        string currencyName = "Dolar";

        var existingCurrencyType = hotelRestaurantDbContext.CurrencyTypes.FirstOrDefault(x => x.CurrencyCode == currencyName);
        if (existingCurrencyType is not null)
        {
            currencyType.Id = existingCurrencyType.Id;
            currencyType.CurrencyCode = existingCurrencyType.CurrencyCode;
        }
        else
        {
            currencyType.Id = Guid.NewGuid();
            currencyType.CurrencyCode = currencyName;

            hotelRestaurantDbContext.CurrencyTypes.Add(currencyType);
            hotelRestaurantDbContext.SaveChanges();
        }


        var restaurantCurrencType = new RestaurantCurrencyType()
        {
            Id = Guid.NewGuid(),
            CurrencyTypeId = currencyType.Id,
            RestaurantId = restaurant.Id
        };

        hotelRestaurantDbContext.RestaurantCurrencyTypes.Add(restaurantCurrencType);

        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateDishes(dynamic record, Restaurant restaurant)
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

            var existingDish = hotelRestaurantDbContext.Dishes.FirstOrDefault(x => x.Name == name);
            if (existingDish is not null)
            {
                dish.Id = existingDish.Id;
                dish.Name = existingDish.Name;
            }

            else
            {
                hotelRestaurantDbContext.Dishes.Add(dish);
                try
                {
                    hotelRestaurantDbContext.SaveChanges();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
                {
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                    Console.WriteLine($"Error Message: {sqlEx.Message}");
                    Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
                }
            }

            RestaurantDishPrice restaurantDishPrice = new RestaurantDishPrice()
            {
                Price = price,
                RestaurantId = restaurant.Id,
                DishId = dish.Id
            };


            hotelRestaurantDbContext.RestaurantDishPrices.Add(restaurantDishPrice);

            try
            {
                hotelRestaurantDbContext.SaveChanges();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                Console.WriteLine("##################################################################");
                Console.WriteLine("Error at record " + recordNumber);
                Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                Console.WriteLine($"Error Message: {sqlEx.Message}");
                Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
            }
        }
    }

    private static void GenerateMealTypes(dynamic record, Restaurant restaurant)
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

            var existingMealType = hotelRestaurantDbContext.MealTypes.FirstOrDefault(x => x.Name == name);
            if (existingMealType is not null)
            {
                mealType.Id = existingMealType.Id;
                mealType.Name = existingMealType.Name;
            }

            else
            {
                hotelRestaurantDbContext.MealTypes.Add(mealType);
                try
                {
                    hotelRestaurantDbContext.SaveChanges();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
                {
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                    Console.WriteLine($"Error Message: {sqlEx.Message}");
                    Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
                }
            }

            RestaurantMealType restaurantMealType = new RestaurantMealType()
            {
                Id= Guid.NewGuid(),
                MealTypeId = mealType.Id,
                RestaurantId = restaurant.Id
            };

            hotelRestaurantDbContext.RestaurantMealTypes.Add(restaurantMealType);

            try
            {
                hotelRestaurantDbContext.SaveChanges();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                Console.WriteLine("##################################################################");
                Console.WriteLine("Error at record " + recordNumber);
                Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                Console.WriteLine($"Error Message: {sqlEx.Message}");
                Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
            }
        }
    }

    private static void GenerateCuisines(dynamic record, Restaurant restaurant)
    {
        string cuisinesInJson = record.CUISINES;
        cuisinesInJson = cuisinesInJson.Replace("'", "\"");

        if (cuisinesInJson is null)
            throw new Exception("Cuisine Is Null.");

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

            var existingCuisine = hotelRestaurantDbContext.Cuisines.FirstOrDefault(x => x.Name == name);
            if (existingCuisine is not null)
            {
                cuisine.Id  = existingCuisine.Id;
                cuisine.Name = existingCuisine.Name;
            }
            else
            {
                hotelRestaurantDbContext.Add(cuisine);
                try
                {
                    hotelRestaurantDbContext.SaveChanges();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
                {
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                    Console.WriteLine($"Error Message: {sqlEx.Message}");
                    Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
                }
            }

            RestaurantCuisine restaurantCuisine = new RestaurantCuisine()
            {
                Id = Guid.NewGuid(),
                CuisineId = cuisine.Id,
                RestaurantId = restaurant.Id
            };

            hotelRestaurantDbContext.RestaurantCuisines.Add(restaurantCuisine);
            try
            {
                hotelRestaurantDbContext.SaveChanges();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                Console.WriteLine("##################################################################");
                Console.WriteLine("Error at record " + recordNumber);
                Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                Console.WriteLine($"Error Message: {sqlEx.Message}");
                Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
            }
        }
    }

    private static void GenerateTags(dynamic record, Restaurant restaurant)
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

            var existingTag = hotelRestaurantDbContext.Tags.FirstOrDefault(x => x.Name == name);
            if (existingTag is not null)
            {
                tag.Id = existingTag.Id;
                tag.Name = existingTag.Name;
            }

            else
            {
                hotelRestaurantDbContext.Tags.Add(tag);
                try
                {
                    hotelRestaurantDbContext.SaveChanges();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
                {
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                    Console.WriteLine($"Error Message: {sqlEx.Message}");
                    Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
                }
            }

            RestaurantTag restaurantTag = new RestaurantTag()
            {
                Id = Guid.NewGuid(),
                TagId = tag.Id,
                RestaurantId = restaurant.Id
            };

            hotelRestaurantDbContext.RestaurantTags.Add(restaurantTag);

            try
            {
                hotelRestaurantDbContext.SaveChanges();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                Console.WriteLine("##################################################################");
                Console.WriteLine("Error at record " + recordNumber);
                Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                Console.WriteLine($"Error Message: {sqlEx.Message}");
                Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
            }
        }
    }

    private static void GenerateFeatures(dynamic record, Restaurant restaurant)
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

            var existingFeature = hotelRestaurantDbContext.Features.FirstOrDefault(x => x.Name == name);
            if (existingFeature is not null)
            {
                feature.Id = existingFeature.Id;
                feature.Name = existingFeature.Name;
            }

            else
            {
                hotelRestaurantDbContext.Features.Add(feature);
                try
                {
                    hotelRestaurantDbContext.SaveChanges();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
                {
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                    Console.WriteLine($"Error Message: {sqlEx.Message}");
                    Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
                }
            }

            RestaurantFeature restaurantFeature = new RestaurantFeature()
            {
                Id = Guid.NewGuid(),
                FeatureId = feature.Id,
                RestaurantId = restaurant.Id
            };

            hotelRestaurantDbContext.RestaurantFeatures.Add(restaurantFeature);
            try
            {
                hotelRestaurantDbContext.SaveChanges();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                Console.WriteLine("##################################################################");
                Console.WriteLine("Error at record " + recordNumber);
                Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                Console.WriteLine($"Error Message: {sqlEx.Message}");
                Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
            }
        }
    }

    private static void GenerateWorkTimes(dynamic record, Restaurant restaurant, JsonSerializerOptions options)
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

                var existingWorkTime = hotelRestaurantDbContext.WorkTimes.FirstOrDefault(x => x.Day == day && x.OpenHour == openHour
                && x.CloseHour == closeHour);

                if (existingWorkTime is not null)
                {
                    workTime.Id = existingWorkTime.Id;
                    workTime.Day = existingWorkTime.Day;
                    workTime.OpenHour = existingWorkTime.OpenHour;
                    workTime.CloseHour = existingWorkTime.CloseHour;
                }

                else
                {
                    hotelRestaurantDbContext.WorkTimes.Add(workTime);
                    try
                    {
                        hotelRestaurantDbContext.SaveChanges();
                    }
                    catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
                    {
                        Console.WriteLine("##################################################################");
                        Console.WriteLine("Error at record " + recordNumber);
                        Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                        Console.WriteLine($"Error Message: {sqlEx.Message}");
                        Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
                    }
                }

                RestaurantWorkTime restaurantWorkTime = new RestaurantWorkTime()
                {
                    Id = Guid.NewGuid(),
                    WorkTimeId = workTime.Id,
                    RestaurantId = restaurant.Id
                };

                hotelRestaurantDbContext.RestaurantWorkTimes.Add(restaurantWorkTime);
                try
                {
                    hotelRestaurantDbContext.SaveChanges();
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
                {
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
                    Console.WriteLine($"Error Message: {sqlEx.Message}");
                    Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
                }
            }
        }
    }

    private static void GenerateRestaurant(dynamic record, Restaurant restaurant, Location location)
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


        var existingHotel = hotelRestaurantDbContext.Restaurants.FirstOrDefault(x => x.Name == name && x.Url == url
        && x.PictureUrl == pictureUrl && x.StarRating == starRating && x.Description == description
        && x.Latitude == latitude && x.Longitude == longitude
        && x.NumberOfTables == numberOfTables && x.LocationId == location.Id);
        if (existingHotel is not null)
        {
            restaurant.Id = existingHotel.Id;
            restaurant.Name = existingHotel.Name;
            restaurant.Url = existingHotel.Url;
            restaurant.PictureUrl = existingHotel.PictureUrl;
            restaurant.StarRating = existingHotel.StarRating;
            restaurant.Description = existingHotel.Description;
            restaurant.Latitude = existingHotel.Latitude;
            restaurant.Longitude = existingHotel.Longitude;
            restaurant.NumberOfTables = existingHotel.NumberOfTables;
            restaurant.PriceLevel = existingHotel.PriceLevel;
            restaurant.MinPrice = existingHotel.MinPrice;
            restaurant.MaxPrice = existingHotel.MaxPrice;
            restaurant.LocationId = existingHotel.LocationId;
        }

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

        hotelRestaurantDbContext.Restaurants.Add(restaurant);
        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateLocation(Country country, CityLocalLocations cityLocalLocations, Location location)
    {
        var existingLocation = hotelRestaurantDbContext.Locations.FirstOrDefault(x => x.CountryId == country.Id
        && x.CityLocalLocationsId == cityLocalLocations.Id);

        if (existingLocation is not null)
        {
            location.Id = existingLocation.Id;
            location.CountryId = existingLocation.CountryId;
            location.CityLocalLocationsId = cityLocalLocations.Id;
            return;
        }

        location.Id = Guid.NewGuid();
        location.CountryId = country.Id;
        location.CityLocalLocationsId = cityLocalLocations.Id;

        hotelRestaurantDbContext.Add(location);
        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateLocalLocation(dynamic record, LocalLocation localLocation, City city,
        CityLocalLocations cityLocalLocations)
    {
        string name = record.GENERAL_LOCATION;

        var existingLocalLocation = hotelRestaurantDbContext.LocalLocations.FirstOrDefault(x => x.Name == name);
        if (existingLocalLocation is not null)
        {
            localLocation.Id = existingLocalLocation.Id;
            localLocation.Name = existingLocalLocation.Name;
        }

        else
        {
            localLocation.Id = Guid.NewGuid();
            localLocation.Name = record.GENERAL_LOCATION;

            hotelRestaurantDbContext.LocalLocations.Add(localLocation);
            hotelRestaurantDbContext.SaveChanges();
        }

        var existingCityLocalLocation = hotelRestaurantDbContext.CityLocalLocations.FirstOrDefault(x => x.CityId == city.Id
        && x.LocalLocationId == localLocation.Id);

        if (existingCityLocalLocation is not null)
        {
            cityLocalLocations.Id= existingCityLocalLocation.Id;
            cityLocalLocations.CityId= existingCityLocalLocation.CityId;
            cityLocalLocations.LocalLocationId= existingCityLocalLocation.LocalLocationId;
            return;
        }


        cityLocalLocations.Id = Guid.NewGuid();
        cityLocalLocations.CityId= city.Id;
        cityLocalLocations.LocalLocationId = localLocation.Id;

        hotelRestaurantDbContext.CityLocalLocations.Add(cityLocalLocations);

        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateCity(dynamic record, City city, Country country)
    {
        string name = record.DESTINATION;

        var existingCity = hotelRestaurantDbContext.Cities.FirstOrDefault(x => x.Name == name);
        if (existingCity is not null)
        {
            city.Id = existingCity.Id;
            city.Name = existingCity.Name;
            city.CountryId = country.Id;
            return;
        }

        city.Id = Guid.NewGuid();
        city.Name = record.DESTINATION;
        city.CountryId = country.Id;

        hotelRestaurantDbContext.Cities.Add(city);
        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateCountry(Country country)
    {
        string countryName = string.Empty;

        var existingCountry = hotelRestaurantDbContext.Countries.FirstOrDefault(x => x.Name == countryName);
        if (existingCountry is not null)
        {
            country.Id = existingCountry.Id;
            countryName = existingCountry.Name;
            return;
        }

        country.Id = Guid.NewGuid();
        country.Name = string.Empty;

        hotelRestaurantDbContext.Countries.Add(country);
        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }
}
