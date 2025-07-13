using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Infrastructure
{
    public class DataSeeder
    {
        private readonly HotelRestaurantDbContext _dbContext;

        public DataSeeder(HotelRestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            if (await _dbContext.Restaurants.AnyAsync())
            {
                return; // DB has been seeded
            }

            var random = new Random();

            // Seed Countries
            var countries = new List<Country>();
            for (int i = 0; i < 10; i++)
            {
                countries.Add(new Country { Id = Guid.NewGuid(), Name = $"Country {i}" });
            }
            await _dbContext.Countries.AddRangeAsync(countries);
            await _dbContext.SaveChangesAsync();

            // Seed Cities
            var cities = new List<City>();
            foreach (var country in countries)
            {
                cities.Add(new City { Id = Guid.NewGuid(), Name = $"City in {country.Name}", CountryId = country.Id });
            }
            await _dbContext.Cities.AddRangeAsync(cities);
            await _dbContext.SaveChangesAsync();

            // Seed LocalLocations
            var localLocations = new List<LocalLocation>();
            for (int i = 0; i < 10; i++)
            {
                localLocations.Add(new LocalLocation { Id = Guid.NewGuid(), Name = $"Local Location {i}" });
            }
            await _dbContext.LocalLocations.AddRangeAsync(localLocations);
            await _dbContext.SaveChangesAsync();

            // Seed CityLocalLocations
            var cityLocalLocations = new List<CityLocalLocations>();
            foreach (var city in cities)
            {
                cityLocalLocations.Add(new CityLocalLocations { Id = Guid.NewGuid(), CityId = city.Id, LocalLocationId = localLocations[random.Next(localLocations.Count)].Id });
            }
            await _dbContext.CityLocalLocations.AddRangeAsync(cityLocalLocations);
            await _dbContext.SaveChangesAsync();

            // Seed Locations
            var locations = new List<Location>();
            foreach (var cll in cityLocalLocations)
            {
                var countryId = cities.First(c => c.Id == cll.CityId).CountryId;
                locations.Add(new Location { Id = Guid.NewGuid(), CountryId = countryId, CityLocalLocationsId = cll.Id });
            }
            await _dbContext.Locations.AddRangeAsync(locations);
            await _dbContext.SaveChangesAsync();

            // Seed Customers
            var customers = new List<Customer>();
            for (int i = 0; i < 10; i++)
            {
                customers.Add(new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    Email = $"customer{i}@example.com",
                    Password = "password", // In a real app, hash this!
                    BirthDate = new DateOnly(1990 + i % 30, 1, 1),
                    Age = 30 + i % 10,
                    Role = Roles.Customer,
                    LocationId = locations[random.Next(locations.Count)].Id
                });
            }
            await _dbContext.Customers.AddRangeAsync(customers);
            await _dbContext.SaveChangesAsync();

            // Seed PropertyTypes
            var propertyTypes = new List<PropertyType>();
            for (int i = 0; i < 10; i++)
            {
                propertyTypes.Add(new PropertyType { Id = Guid.NewGuid(), Name = $"Property Type {i}" });
            }
            await _dbContext.PropertyTypes.AddRangeAsync(propertyTypes);
            await _dbContext.SaveChangesAsync();

            // Seed Hotels
            var hotels = new List<Hotel>();
            for (int i = 0; i < 10; i++)
            {
                hotels.Add(new Hotel
                {
                    Id = Guid.NewGuid(),
                    Name = $"Hotel {i}",
                    Url = $"http://hotel{i}.com",
                    StarRate = random.Next(1, 6),
                    NumberOfRooms = 0,
                    Latitude = random.NextDouble() * 180 - 90,
                    Longitude = random.NextDouble() * 360 - 180,
                    PropertyTypeId = propertyTypes[random.Next(propertyTypes.Count)].Id,
                    LocationId = locations[random.Next(locations.Count)].Id
                });
            }
            await _dbContext.Hotels.AddRangeAsync(hotels);
            await _dbContext.SaveChangesAsync();

            // Seed RoomTypes
            var roomTypes = new List<RoomType>();
            for (int i = 0; i < 10; i++)
            {
                roomTypes.Add(new RoomType { Id = Guid.NewGuid(), Description = $"Room Type {i}" });
            }
            await _dbContext.RoomTypes.AddRangeAsync(roomTypes);
            await _dbContext.SaveChangesAsync();


            // Seed Rooms
            var rooms = new List<Room>();
            foreach (var hotel in hotels)
            {
                rooms.Add(new Room
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = random.Next(1, 101),
                    MaxOccupancy = random.Next(1, 5),
                    Description = $"Room in {hotel.Name}",
                    Price = random.Next(50, 201),
                    HotelId = hotel.Id,
                    RoomTypeId = roomTypes[random.Next(roomTypes.Count)].Id
                });
            }
            await _dbContext.Rooms.AddRangeAsync(rooms);
            await _dbContext.SaveChangesAsync();

            // Seed Amenities
            var amenities = new List<Amenity>();
            for (int i = 0; i < 10; i++)
            {
                amenities.Add(new Amenity { Id = Guid.NewGuid(), Name = $"Amenity {i}" });
            }
            await _dbContext.Amenities.AddRangeAsync(amenities);
            await _dbContext.SaveChangesAsync();

            // Seed HotelAmenityPrices
            var hotelAmenityPrices = new List<HotelAmenityPrice>();
            foreach (var hotel in hotels)
            {
                hotelAmenityPrices.Add(new HotelAmenityPrice
                {
                    Id = Guid.NewGuid(),
                    HotelId = hotel.Id,
                    AmenityId = amenities[random.Next(amenities.Count)].Id,
                    Price = random.Next(10, 51)
                });
            }
            await _dbContext.AddRangeAsync(hotelAmenityPrices);
            await _dbContext.SaveChangesAsync();

            // Seed RoomAmenities
            var roomAmenities = new List<RoomAmenity>();
            foreach (var room in rooms)
            {
                roomAmenities.Add(new RoomAmenity
                {
                    Id = Guid.NewGuid(),
                    RoomId = room.Id,
                    AmenityId = amenities[random.Next(amenities.Count)].Id
                });
            }
            await _dbContext.RoomAmenities.AddRangeAsync(roomAmenities);
            await _dbContext.SaveChangesAsync();

            // Seed HotelReviews
            var hotelReviews = new List<HotelReview>();
            foreach (var hotel in hotels)
            {
                hotelReviews.Add(new HotelReview
                {
                    Id = Guid.NewGuid(),
                    HotelId = hotel.Id,
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    ReviewDateTime = DateTime.UtcNow,
                    Description = $"Review for {hotel.Name}",
                    OverallRating = random.Next(1, 6),
                    ServiceRating = random.Next(1, 6),
                    CleanlinessRating = random.Next(1, 6),
                    ValueRating = random.Next(1, 6)
                });
            }
            await _dbContext.AddRangeAsync(hotelReviews);
            await _dbContext.SaveChangesAsync();

            // Seed HotelReservations
            var hotelReservations = new List<HotelReservation>();
            for (int i = 0; i < 10; i++)
            {
                hotelReservations.Add(new HotelReservation
                {
                    Id = Guid.NewGuid(),
                    ReservationDateTime = DateTime.UtcNow.AddDays(-random.Next(1, 30)),
                    ReceivationStartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(random.Next(1, 10))),
                    ReceivationEndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(random.Next(11, 20))),
                    NumberOfPeople = random.Next(1, 5),
                    HotelId = hotels[random.Next(hotels.Count)].Id,
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    RoomId = rooms[random.Next(rooms.Count)].Id
                });
            }
            await _dbContext.HotelReservations.AddRangeAsync(hotelReservations);
            await _dbContext.SaveChangesAsync();

            // Seed CurrencyTypes
            var currencyTypes = new List<CurrencyType>();
            for (int i = 0; i < 10; i++)
            {
                currencyTypes.Add(new CurrencyType { Id = Guid.NewGuid(), CurrencyCode = $"CUR{i}" });
            }
            await _dbContext.CurrencyTypes.AddRangeAsync(currencyTypes);
            await _dbContext.SaveChangesAsync();

            // Seed Events
            var events = new List<Event>();
            for (int i = 0; i < 10; i++)
            {
                events.Add(new Event
                {
                    Id = Guid.NewGuid(),
                    Name = $"Event {i}",
                    Description = $"Description for Event {i}",
                    StartingDateTime = DateTime.UtcNow.AddDays(random.Next(10, 20)),
                    EndDateTime = DateTime.UtcNow.AddDays(random.Next(21, 30)),
                    PayToEnter = random.Next(0, 101),
                    MaxNumberOfRegesters = random.Next(50, 201),
                    LocationId = locations[random.Next(locations.Count)].Id,
                    CurrencyTypeId = currencyTypes[random.Next(currencyTypes.Count)].Id
                });
            }
            await _dbContext.Events.AddRangeAsync(events);
            await _dbContext.SaveChangesAsync();

            // Seed EventReviews
            var eventReviews = new List<EventReview>();
            foreach (var ev in events)
            {
                eventReviews.Add(new EventReview
                {
                    Id = Guid.NewGuid(),
                    EventId = ev.Id,
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    ReviewDateTime = DateTime.UtcNow,
                    Description = $"Review for {ev.Name}",
                    Rating = random.Next(1, 6)
                });
            }
            await _dbContext.AddRangeAsync(eventReviews);
            await _dbContext.SaveChangesAsync();

            // Seed EventRegistrations
            var eventRegistrations = new List<EventRegistration>();
            foreach (var ev in events)
            {
                eventRegistrations.Add(new EventRegistration
                {
                    Id = Guid.NewGuid(),
                    RegistrationDateTime = DateTime.UtcNow.AddDays(-random.Next(1, 5)),
                    NumberOfPeople = random.Next(1, 4),
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    EventId = ev.Id
                });
            }
            await _dbContext.EventRegistrations.AddRangeAsync(eventRegistrations);
            await _dbContext.SaveChangesAsync();


            // Seed Cuisines
            var cuisines = new List<Cuisine>();
            for (int i = 0; i < 10; i++)
            {
                cuisines.Add(new Cuisine { Id = Guid.NewGuid(), Name = $"Cuisine {i}" });
            }
            await _dbContext.Cuisines.AddRangeAsync(cuisines);
            await _dbContext.SaveChangesAsync();

            // Seed Dishes
            var dishes = new List<Dish>();
            for (int i = 0; i < 10; i++)
            {
                dishes.Add(new Dish { Id = Guid.NewGuid(), Name = $"Dish {i}" });
            }
            await _dbContext.Dishes.AddRangeAsync(dishes);
            await _dbContext.SaveChangesAsync();

            // Seed Features
            var features = new List<Feature>();
            for (int i = 0; i < 10; i++)
            {
                features.Add(new Feature { Id = Guid.NewGuid(), Name = $"Feature {i}" });
            }
            await _dbContext.Features.AddRangeAsync(features);
            await _dbContext.SaveChangesAsync();

            // Seed MealTypes
            var mealTypes = new List<MealType>();
            for (int i = 0; i < 10; i++)
            {
                mealTypes.Add(new MealType { Id = Guid.NewGuid(), Name = $"Meal Type {i}" });
            }
            await _dbContext.MealTypes.AddRangeAsync(mealTypes);
            await _dbContext.SaveChangesAsync();

            // Seed Tags
            var tags = new List<Tag>();
            for (int i = 0; i < 10; i++)
            {
                tags.Add(new Tag { Id = Guid.NewGuid(), Name = $"Tag {i}" });
            }
            await _dbContext.Tags.AddRangeAsync(tags);
            await _dbContext.SaveChangesAsync();

            // Seed WorkTimes
            var workTimes = new List<WorkTime>();
            for (int i = 0; i < 10; i++)
            {
                workTimes.Add(new WorkTime
                {
                    Id = Guid.NewGuid(),
                    Day = (DayOfWeek)random.Next(7),
                    OpenHour = new TimeOnly(random.Next(8, 12), 0),
                    CloseHour = new TimeOnly(random.Next(18, 23), 0)
                });
            }
            await _dbContext.WorkTimes.AddRangeAsync(workTimes);
            await _dbContext.SaveChangesAsync();

            // Seed Restaurants
            var restaurants = new List<Restaurant>();
            for (int i = 0; i < 10; i++)
            {
                restaurants.Add(new Restaurant
                {
                    Id = Guid.NewGuid(),
                    Name = $"Restaurant {i}",
                    Description = $"Description for Restaurant {i}",
                    Url = $"http://restaurant{i}.com",
                    PictureUrl = $"http://restaurant{i}.com/pic.jpg",
                    StarRating = random.Next(1, 6),
                    Latitude = random.NextDouble() * 180 - 90,
                    Longitude = random.NextDouble() * 360 - 180,
                    NumberOfTables = random.Next(5, 21),
                    PriceLevel = (RestaurantPriceLevel)random.Next(1, 5),
                    MinPrice = random.Next(10, 21),
                    MaxPrice = random.Next(50, 101),
                    LocationId = locations[random.Next(locations.Count)].Id
                });
            }
            await _dbContext.Restaurants.AddRangeAsync(restaurants);
            await _dbContext.SaveChangesAsync();

            // Seeding for join tables
            var restaurantCuisines = new List<RestaurantCuisine>();
            foreach (var r in restaurants)
            {
                restaurantCuisines.Add(new RestaurantCuisine { Id = Guid.NewGuid(), RestaurantId = r.Id, CuisineId = cuisines[random.Next(cuisines.Count)].Id });
            }
            await _dbContext.RestaurantCuisines.AddRangeAsync(restaurantCuisines);

            var restaurantDishPrices = new List<RestaurantDishPrice>();
            foreach (var r in restaurants)
            {
                restaurantDishPrices.Add(new RestaurantDishPrice { Id = Guid.NewGuid(), RestaurantId = r.Id, DishId = dishes[random.Next(dishes.Count)].Id, Price = random.Next(5, 50) });
            }
            await _dbContext.RestaurantDishPrices.AddRangeAsync(restaurantDishPrices);

            var restaurantFeatures = new List<RestaurantFeature>();
            foreach (var r in restaurants)
            {
                restaurantFeatures.Add(new RestaurantFeature { Id = Guid.NewGuid(), RestaurantId = r.Id, FeatureId = features[random.Next(features.Count)].Id });
            }
            await _dbContext.RestaurantFeatures.AddRangeAsync(restaurantFeatures);

            var restaurantMealTypes = new List<RestaurantMealType>();
            foreach (var r in restaurants)
            {
                restaurantMealTypes.Add(new RestaurantMealType { Id = Guid.NewGuid(), RestaurantId = r.Id, MealTypeId = mealTypes[random.Next(mealTypes.Count)].Id });
            }
            await _dbContext.RestaurantMealTypes.AddRangeAsync(restaurantMealTypes);

            var restaurantTags = new List<RestaurantTag>();
            foreach (var r in restaurants)
            {
                restaurantTags.Add(new RestaurantTag { Id = Guid.NewGuid(), RestaurantId = r.Id, TagId = tags[random.Next(tags.Count)].Id });
            }
            await _dbContext.RestaurantTags.AddRangeAsync(restaurantTags);

            var restaurantWorkTimes = new List<RestaurantWorkTime>();
            foreach (var r in restaurants)
            {
                restaurantWorkTimes.Add(new RestaurantWorkTime { Id = Guid.NewGuid(), RestaurantId = r.Id, WorkTimeId = workTimes[random.Next(workTimes.Count)].Id });
            }
            await _dbContext.RestaurantWorkTimes.AddRangeAsync(restaurantWorkTimes);

            var restaurantReviews = new List<RestaurantReview>();
            foreach (var r in restaurants)
            {
                restaurantReviews.Add(new RestaurantReview
                {
                    Id = Guid.NewGuid(),
                    RestaurantId = r.Id,
                    CustomerId = customers[random.Next(customers.Count)].Id,
                    ReviewDateTime = DateTime.UtcNow.AddDays(-random.Next(1, 100)),
                    Description = $"A great place to eat!",
                    CustomerStarRating = random.Next(1, 6),
                    CustomerServiceStarRating = random.Next(1, 6),
                    CustomerFoodStarRating = random.Next(1, 6)
                });
            }
            await _dbContext.RestaurantReviews.AddRangeAsync(restaurantReviews);

            var restaurantBookings = new List<RestaurantBooking>();
            for (int i = 0; i < 10; i++)
            {
                restaurantBookings.Add(new RestaurantBooking
                {
                    Id = Guid.NewGuid(),
                    BookingDateTime = DateTime.UtcNow.AddDays(-random.Next(1, 10)),
                    ReceiveDateTime = DateTime.UtcNow.AddDays(random.Next(1, 5)),
                    BookingDurationTime = new TimeOnly(1, 30, 0),
                    NumberOfPeople = random.Next(1, 5),
                    TableNumber = random.Next(1, 20),
                    RestaurantId = restaurants[random.Next(restaurants.Count)].Id,
                    CustomerId = customers[random.Next(customers.Count)].Id
                });
            }
            await _dbContext.RestaurantBookings.AddRangeAsync(restaurantBookings);
            await _dbContext.SaveChangesAsync();

            var bookingDishes = new List<BookingDish>();
            foreach (var booking in restaurantBookings)
            {
                bookingDishes.Add(new BookingDish
                {
                    Id = Guid.NewGuid(),
                    RestaurantBookingId = booking.Id,
                    DishId = dishes[random.Next(dishes.Count)].Id,
                    Quantity = random.Next(1, 4)
                });
            }
            await _dbContext.AddRangeAsync(bookingDishes);

            var restaurantCurrencyTypes = new List<RestaurantCurrencyType>();
            foreach (var r in restaurants)
            {
                restaurantCurrencyTypes.Add(new RestaurantCurrencyType { Id = Guid.NewGuid(), RestaurantId = r.Id, CurrencyTypeId = currencyTypes[random.Next(currencyTypes.Count)].Id });
            }
            await _dbContext.RestaurantCurrencyTypes.AddRangeAsync(restaurantCurrencyTypes);


            await _dbContext.SaveChangesAsync();
        }
    }
}