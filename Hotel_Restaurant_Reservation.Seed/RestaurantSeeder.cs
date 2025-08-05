// YelpDataSeeder.cs
using Bogus;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Mappings;
using Hotel_Restaurant_Reservation.Infrastructure;
// --- C H A N G E: Added using statement for PasswordHasher ---
using Hotel_Restaurant_Reservation.Infrastructure.PasswordHasher;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Seed
{
    #region Yelp Deserialization Classes
    public class YelpBusiness
    {
        [JsonProperty("business_id")]
        public string BusinessId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }
        [JsonProperty("longitude")]
        public double? Longitude { get; set; }
        [JsonProperty("stars")]
        public double Stars { get; set; }
        [JsonProperty("review_count")]
        public int ReviewCount { get; set; }
        [JsonProperty("categories")]
        public string Categories { get; set; }
        [JsonProperty("hours")]
        public Dictionary<string, string> Hours { get; set; }
        [JsonProperty("attributes")]
        public Dictionary<string, object> Attributes { get; set; }
    }

    public class YelpReview
    {
        [JsonProperty("review_id")]
        public string ReviewId { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("business_id")]
        public string BusinessId { get; set; }
        [JsonProperty("stars")]
        public double Stars { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
    #endregion

    public class YelpDataSeeder
    {
        private readonly HotelRestaurantDbContext _context;
        private readonly Faker _faker;
        // --- C H A N G E: Added a field for the PasswordHasher ---
        private readonly PasswordHasher _passwordHasher;
        private readonly Dictionary<string, Country> _countries = new Dictionary<string, Country>();
        private readonly Dictionary<string, City> _cities = new Dictionary<string, City>();
        private readonly Dictionary<string, Cuisine> _cuisines = new Dictionary<string, Cuisine>();
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();
        private readonly Dictionary<string, Restaurant> _restaurants = new Dictionary<string, Restaurant>();

        public YelpDataSeeder(HotelRestaurantDbContext context)
        {
            _context = context;
            _faker = new Faker();
            // --- C H A N G E: Instantiated the PasswordHasher ---
            _passwordHasher = new PasswordHasher();
        }

        public async Task SeedRestaurantsAsync(string businessFilePath, int maxRecords = 1000)
        {
            Console.WriteLine("Starting to seed restaurants and mappings...");
            if (await _context.Restaurants.AnyAsync())
            {
                Console.WriteLine("Restaurants table already contains data. Skipping restaurant seeding.");
                var existingMappings = await _context.RestaurantMappings.Include(rm => rm.Restaurant).Take(maxRecords).ToListAsync();
                foreach (var mapping in existingMappings) { _restaurants.TryAdd(mapping.YelpBusinessId, mapping.Restaurant); }
                return;
            }

            var newRestaurantMappings = new List<RestaurantMapping>();
            var newRestaurantCuisines = new List<RestaurantCuisine>();
            var newRestaurantTags = new List<RestaurantTag>();
            var newRestaurantWorkTimes = new List<RestaurantWorkTime>();

            using (var reader = new StreamReader(businessFilePath))
            {
                string line;
                int count = 0;
                while ((line = reader.ReadLine()) != null && count < maxRecords)
                {
                    var yelpBusiness = JsonConvert.DeserializeObject<YelpBusiness>(line);
                    if (string.IsNullOrEmpty(yelpBusiness.Categories) || !yelpBusiness.Categories.ToLower().Contains("restaurant")) continue;

                    var location = await GetOrCreateLocationAsync(yelpBusiness);
                    var priceLevel = GetPriceLevel(yelpBusiness.Attributes);
                    var (minPrice, maxPrice) = GetPriceRange(priceLevel);

                    var restaurant = new Restaurant
                    {
                        Id = Guid.NewGuid(),
                        Name = yelpBusiness.Name,
                        Description = $"A popular restaurant in {yelpBusiness.City} known for its vibrant atmosphere.",
                        Url = _faker.Internet.Url(),
                        PictureUrl = _faker.Image.PicsumUrl(),
                        StarRating = yelpBusiness.Stars,
                        Latitude = yelpBusiness.Latitude ?? 0,
                        Longitude = yelpBusiness.Longitude ?? 0,
                        NumberOfTables = _faker.Random.Int(10, 50),
                        PriceLevel = priceLevel,
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        LocationId = location.Id
                    };
                    _restaurants.TryAdd(yelpBusiness.BusinessId, restaurant);

                    var restaurantMapping = new RestaurantMapping
                    {
                        YelpBusinessId = yelpBusiness.BusinessId,
                        Restaurant = restaurant
                    };
                    newRestaurantMappings.Add(restaurantMapping);

                    var categories = yelpBusiness.Categories.Split(',').Select(c => c.Trim()).ToList();
                    foreach (var categoryName in categories)
                    {
                        var cuisine = await GetOrCreateCuisineAsync(categoryName);
                        newRestaurantCuisines.Add(new RestaurantCuisine { Id = Guid.NewGuid(), Restaurant = restaurant, Cuisine = cuisine });
                        var tag = await GetOrCreateTagAsync(categoryName);
                        newRestaurantTags.Add(new RestaurantTag { Id = Guid.NewGuid(), Restaurant = restaurant, Tag = tag });
                    }

                    if (yelpBusiness.Hours != null)
                    {
                        foreach (var hour in yelpBusiness.Hours)
                        {
                            var workTime = ParseWorkTime(hour.Key, hour.Value, restaurant.Id);
                            if (workTime != null) newRestaurantWorkTimes.Add(workTime);
                        }
                    }
                    count++;
                }
            }

            Console.WriteLine($"Processed {newRestaurantMappings.Count} restaurants. Saving to database...");

            await _context.AddRangeAsync(newRestaurantMappings);
            await _context.AddRangeAsync(newRestaurantCuisines);
            await _context.AddRangeAsync(newRestaurantTags);
            await _context.AddRangeAsync(newRestaurantWorkTimes);

            await _context.SaveChangesAsync();
            Console.WriteLine("Finished seeding restaurants and their mappings.");
        }

        public async Task SeedReviewsAsync(string reviewFilePath, int maxRecords = 5000)
        {
            Console.WriteLine("Starting to seed reviews and user mappings...");
            var newReviews = new List<RestaurantReview>();
            var newUserMappings = new List<UserMapping>();

            var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
            if (userRole == null)
            {
                userRole = new Role { Id = Guid.NewGuid(), Name = "Customer" };
                _context.Roles.Add(userRole);
                await _context.SaveChangesAsync();
            }

            using (var reader = new StreamReader(reviewFilePath))
            {
                string line;
                int count = 0;
                while ((line = reader.ReadLine()) != null && count < maxRecords)
                {
                    var yelpReview = JsonConvert.DeserializeObject<YelpReview>(line);
                    if (_restaurants.TryGetValue(yelpReview.BusinessId, out var restaurant))
                    {
                        bool isNewUser = !_users.ContainsKey(yelpReview.UserId);
                        var user = await GetOrCreateUserAsync(yelpReview.UserId, userRole.Id);

                        if (isNewUser)
                        {
                            // --- C H A N G E: Modified UserMapping creation to fit new entity structure ---
                            var userMapping = new UserMapping
                            {
                                Id = Guid.NewGuid(), // Explicitly create a new Id for the mapping record
                                UserId = user.Id,    // Set the foreign key
                                YelpUserId = yelpReview.UserId,
                                User = user          // Assign the navigation property
                            };
                            newUserMappings.Add(userMapping);
                        }

                        var review = new RestaurantReview
                        {
                            Id = Guid.NewGuid(),
                            Restaurant = restaurant,
                            User = user,
                            ReviewDateTime = yelpReview.Date,
                            Description = yelpReview.Text,
                            CustomerStarRating = yelpReview.Stars,
                            CustomerServiceStarRating = Math.Clamp(_faker.Random.Double(yelpReview.Stars - 1, yelpReview.Stars + 1), 1, 5),
                            CustomerFoodStarRating = Math.Clamp(_faker.Random.Double(yelpReview.Stars - 0.5, yelpReview.Stars + 0.5), 1, 5)
                        };
                        newReviews.Add(review);
                        count++;
                    }
                }
            }

            Console.WriteLine($"Processed {newReviews.Count} reviews for {newUserMappings.Count} new users. Saving to database...");
            await _context.AddRangeAsync(newUserMappings);
            await _context.AddRangeAsync(newReviews);
            await _context.SaveChangesAsync();
            Console.WriteLine("Finished seeding reviews and user mappings.");
        }

        #region Helper Methods
        private async Task<Cuisine> GetOrCreateCuisineAsync(string name)
        {
            if (_cuisines.TryGetValue(name, out var cuisine)) return cuisine;
            cuisine = await _context.Cuisines.FirstOrDefaultAsync(c => c.Name == name);
            if (cuisine == null)
            {
                cuisine = new Cuisine { Id = Guid.NewGuid(), Name = name };
                _context.Cuisines.Add(cuisine);
            }
            _cuisines.TryAdd(name, cuisine);
            return cuisine;
        }

        private async Task<Tag> GetOrCreateTagAsync(string name)
        {
            if (_tags.TryGetValue(name, out var tag)) return tag;
            tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
            if (tag == null)
            {
                tag = new Tag { Id = Guid.NewGuid(), Name = name };
                _context.Tags.Add(tag);
            }
            _tags.TryAdd(name, tag);
            return tag;
        }

        private async Task<User> GetOrCreateUserAsync(string yelpUserId, Guid roleId)
        {
            if (_users.TryGetValue(yelpUserId, out var user)) return user;

            var location = await _context.Locations.FirstAsync();

            // --- C H A N G E: Hash the default password ---
            var plainPassword = "12345";
            var hashedPassword = _passwordHasher.Hash(plainPassword);

            user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                // --- C H A N G E: Assign the hashed password ---
                HashedPassword = hashedPassword,
                BirthDate = DateOnly.FromDateTime(_faker.Person.DateOfBirth),
                Age = _faker.Random.Int(18, 70),
                RoleId = roleId,
                LocationId = location.Id
            };
            _users.TryAdd(yelpUserId, user);
            return user;
        }

        private async Task<Location> GetOrCreateLocationAsync(YelpBusiness business)
        {
            if (!_countries.TryGetValue("United States", out var country))
            {
                country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == "United States");
                if (country == null)
                {
                    country = new Country { Id = Guid.NewGuid(), Name = "United States" };
                    _context.Countries.Add(country);
                }
                _countries.Add("United States", country);
            }

            if (!_cities.TryGetValue(business.City, out var city))
            {
                city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == business.City && c.CountryId == country.Id);
                if (city == null)
                {
                    city = new City { Id = Guid.NewGuid(), Name = business.City, CountryId = country.Id };
                    _context.Cities.Add(city);
                }
                _cities.Add(business.City, city);
            }

            var localLocationName = $"{business.City} Downtown";
            var localLocation = await _context.LocalLocations.FirstOrDefaultAsync(l => l.Name == localLocationName);
            if (localLocation == null)
            {
                localLocation = new LocalLocation { Id = Guid.NewGuid(), Name = localLocationName };
                _context.LocalLocations.Add(localLocation);
            }

            var cityLocalLocation = await _context.CityLocalLocations.FirstOrDefaultAsync(cl => cl.CityId == city.Id && cl.LocalLocationId == localLocation.Id);
            if (cityLocalLocation == null)
            {
                cityLocalLocation = new CityLocalLocations { Id = Guid.NewGuid(), CityId = city.Id, LocalLocationId = localLocation.Id };
                _context.CityLocalLocations.Add(cityLocalLocation);
            }

            var location = new Location { Id = Guid.NewGuid(), CountryId = country.Id, CityLocalLocationsId = cityLocalLocation.Id };
            _context.Locations.Add(location);

            return location;
        }

        private RestaurantPriceLevel GetPriceLevel(Dictionary<string, object> attributes)
        {
            if (attributes != null && attributes.TryGetValue("RestaurantsPriceRange2", out var priceObj))
            {
                switch (priceObj.ToString())
                {
                    case "1": return RestaurantPriceLevel.Low;
                    case "2": return RestaurantPriceLevel.Medium;
                    case "3": return RestaurantPriceLevel.High;
                    case "4": return RestaurantPriceLevel.Luxury;
                }
            }
            return RestaurantPriceLevel.Medium;
        }

        private (double min, double max) GetPriceRange(RestaurantPriceLevel level)
        {
            return level switch
            {
                RestaurantPriceLevel.Low => (_faker.Random.Double(5, 10), _faker.Random.Double(11, 20)),
                RestaurantPriceLevel.Medium => (_faker.Random.Double(20, 35), _faker.Random.Double(36, 50)),
                RestaurantPriceLevel.High => (_faker.Random.Double(50, 70), _faker.Random.Double(71, 100)),
                RestaurantPriceLevel.Luxury => (_faker.Random.Double(100, 150), _faker.Random.Double(151, 300)),
                _ => (20, 50)
            };
        }

        private RestaurantWorkTime ParseWorkTime(string day, string hours, Guid restaurantId)
        {
            try
            {
                var parts = hours.Split('-');
                if (parts.Length != 2) return null;
                var openHour = TimeOnly.Parse(parts[0]);
                var closeHour = TimeOnly.Parse(parts[1]);
                return new RestaurantWorkTime { Id = Guid.NewGuid(), Day = day, OpenHour = openHour, CloseHour = closeHour, RestaurantId = restaurantId };
            }
            catch (FormatException)
            {
                return null;
            }
        }
        #endregion
    }
}
