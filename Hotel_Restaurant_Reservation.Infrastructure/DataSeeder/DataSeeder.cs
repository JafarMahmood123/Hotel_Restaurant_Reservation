using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Infrastructure;

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

        // Seed Roles
        var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "HotelManager" },
                new Role { Id = Guid.NewGuid(), Name = "RestaurantManager" },
                new Role { Id = Guid.NewGuid(), Name = "EventManager" },
                new Role { Id = Guid.NewGuid(), Name = "Customer" }
            };
        await _dbContext.Roles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        // Seed Countries
        var countries = new List<Country>
            {
                new Country { Id = Guid.NewGuid(), Name = "USA" },
                new Country { Id = Guid.NewGuid(), Name = "Canada" }
            };
        await _dbContext.Countries.AddRangeAsync(countries);
        await _dbContext.SaveChangesAsync();

        // Seed Cities
        var cities = new List<City>
            {
                new City { Id = Guid.NewGuid(), Name = "New York", CountryId = countries[0].Id },
                new City { Id = Guid.NewGuid(), Name = "Los Angeles", CountryId = countries[0].Id },
                new City { Id = Guid.NewGuid(), Name = "Toronto", CountryId = countries[1].Id }
            };
        await _dbContext.Cities.AddRangeAsync(cities);
        await _dbContext.SaveChangesAsync();

        // Seed LocalLocations
        var localLocations = new List<LocalLocation>
            {
                new LocalLocation { Id = Guid.NewGuid(), Name = "Manhattan" },
                new LocalLocation { Id = Guid.NewGuid(), Name = "Hollywood" },
                new LocalLocation { Id = Guid.NewGuid(), Name = "Downtown" }
            };
        await _dbContext.LocalLocations.AddRangeAsync(localLocations);
        await _dbContext.SaveChangesAsync();

        // Seed CityLocalLocations
        var cityLocalLocations = new List<CityLocalLocations>
            {
                new CityLocalLocations { Id = Guid.NewGuid(), CityId = cities[0].Id, LocalLocationId = localLocations[0].Id },
                new CityLocalLocations { Id = Guid.NewGuid(), CityId = cities[1].Id, LocalLocationId = localLocations[1].Id },
                new CityLocalLocations { Id = Guid.NewGuid(), CityId = cities[2].Id, LocalLocationId = localLocations[2].Id }
            };
        await _dbContext.CityLocalLocations.AddRangeAsync(cityLocalLocations);
        await _dbContext.SaveChangesAsync();

        // Seed Locations
        var locations = new List<Location>
            {
                new Location { Id = Guid.NewGuid(), CountryId = countries[0].Id, CityLocalLocationsId = cityLocalLocations[0].Id },
                new Location { Id = Guid.NewGuid(), CountryId = countries[0].Id, CityLocalLocationsId = cityLocalLocations[1].Id },
                new Location { Id = Guid.NewGuid(), CountryId = countries[1].Id, CityLocalLocationsId = cityLocalLocations[2].Id }
            };
        await _dbContext.Locations.AddRangeAsync(locations);
        await _dbContext.SaveChangesAsync();

        // Seed Users
        var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", HashedPassword = "password", BirthDate = new DateOnly(1990, 1, 1), Age = 34, RoleId = roles.First(r => r.Name == "Customer").Id, LocationId = locations[0].Id },
                new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", HashedPassword = "password", BirthDate = new DateOnly(1992, 2, 2), Age = 32, RoleId = roles.First(r => r.Name == "Customer").Id, LocationId = locations[1].Id },
                new User { Id = Guid.NewGuid(), FirstName = "Hotel", LastName = "Manager", Email = "hotel.manager@example.com", HashedPassword = "password", BirthDate = new DateOnly(1985, 5, 5), Age = 39, RoleId = roles.First(r => r.Name == "HotelManager").Id, LocationId = locations[0].Id },
                new User { Id = Guid.NewGuid(), FirstName = "Restaurant", LastName = "Manager", Email = "restaurant.manager@example.com", HashedPassword = "password", BirthDate = new DateOnly(1988, 8, 8), Age = 36, RoleId = roles.First(r => r.Name == "RestaurantManager").Id, LocationId = locations[1].Id },
                new User { Id = Guid.NewGuid(), FirstName = "Event", LastName = "Manager", Email = "event.manager@example.com", HashedPassword = "password", BirthDate = new DateOnly(1991, 1, 1), Age = 33, RoleId = roles.First(r => r.Name == "EventManager").Id, LocationId = locations[2].Id }
            };
        await _dbContext.Users.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync();

        // Seed HotelManagers
        var hotelManagers = new List<HotelManager>();
        var hotelManagerUser = users.First(u => u.Role.Name == "HotelManager");
        hotelManagers.Add(new HotelManager { Id = Guid.NewGuid(), UserId = hotelManagerUser.Id });
        await _dbContext.HotelManagers.AddRangeAsync(hotelManagers);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantManagers
        var restaurantManagers = new List<RestaurantManager>();
        var restaurantManagerUser = users.First(u => u.Role.Name == "RestaurantManager");
        restaurantManagers.Add(new RestaurantManager { Id = Guid.NewGuid(), UserId = restaurantManagerUser.Id });
        await _dbContext.RestaurantManagers.AddRangeAsync(restaurantManagers);
        await _dbContext.SaveChangesAsync();

        // Seed EventManagers
        var eventManagers = new List<EventManager>();
        var eventManagerUser = users.First(u => u.Role.Name == "EventManager");
        eventManagers.Add(new EventManager { Id = Guid.NewGuid(), UserId = eventManagerUser.Id });
        await _dbContext.EventManagers.AddRangeAsync(eventManagers);
        await _dbContext.SaveChangesAsync();

        // Seed PropertyTypes
        var propertyTypes = new List<PropertyType>
            {
                new PropertyType { Id = Guid.NewGuid(), Name = "Hotel" },
                new PropertyType { Id = Guid.NewGuid(), Name = "Motel" }
            };
        await _dbContext.PropertyTypes.AddRangeAsync(propertyTypes);
        await _dbContext.SaveChangesAsync();

        // Seed Hotels
        var hotels = new List<Hotel>
            {
                new Hotel { Id = Guid.NewGuid(), Name = "Grand Hyatt", Url = "http://grandhyatt.com", StarRate = 5, NumberOfRooms = 0, Latitude = 40.7128, Longitude = -74.0060, PropertyTypeId = propertyTypes[0].Id, LocationId = locations[0].Id, MinPrice = 200, MaxPrice = 500, HotelManagerId = hotelManagers[0].Id },
                new Hotel { Id = Guid.NewGuid(), Name = "Hollywood Hotel", Url = "http://hollywoodhotel.com", StarRate = 4, NumberOfRooms = 0, Latitude = 34.0928, Longitude = -118.3287, PropertyTypeId = propertyTypes[0].Id, LocationId = locations[1].Id, MinPrice = 150, MaxPrice = 400, HotelManagerId = hotelManagers[0].Id }
            };
        await _dbContext.Hotels.AddRangeAsync(hotels);
        await _dbContext.SaveChangesAsync();

        // Seed Restaurants
        var restaurants = new List<Restaurant>
            {
                new Restaurant { Id = Guid.NewGuid(), Name = "Italiano's", Description = "Authentic Italian food", Url = "http://italianos.com", PictureUrl = "http://italianos.com/pic.jpg", StarRating = 4.5, Latitude = 40.7128, Longitude = -74.0060, NumberOfTables = 20, PriceLevel = RestaurantPriceLevel.Medium, MinPrice = 15, MaxPrice = 50, LocationId = locations[0].Id, RestaurantManagerId = restaurantManagers[0].Id },
                new Restaurant { Id = Guid.NewGuid(), Name = "Taco Town", Description = "The best tacos in town", Url = "http://tacotown.com", PictureUrl = "http://tacotown.com/pic.jpg", StarRating = 4.0, Latitude = 34.0928, Longitude = -118.3287, NumberOfTables = 15, PriceLevel = RestaurantPriceLevel.Low, MinPrice = 5, MaxPrice = 20, LocationId = locations[1].Id, RestaurantManagerId = restaurantManagers[0].Id }
            };
        await _dbContext.Restaurants.AddRangeAsync(restaurants);
        await _dbContext.SaveChangesAsync();

        // Seed Events
        var events = new List<Event>
            {
                new Event { Id = Guid.NewGuid(), Name = "Music Festival", Description = "A festival of music.", StartingDateTime = DateTime.UtcNow.AddDays(30), EndDateTime = DateTime.UtcNow.AddDays(32), PayToEnter = 50, MaxNumberOfRegesters = 500, LocationId = locations[2].Id, EventManagerId = eventManagers[0].Id },
                new Event { Id = Guid.NewGuid(), Name = "Food Fair", Description = "A fair for foodies.", StartingDateTime = DateTime.UtcNow.AddDays(60), EndDateTime = DateTime.UtcNow.AddDays(61), PayToEnter = 20, MaxNumberOfRegesters = 300, LocationId = locations[2].Id, EventManagerId = eventManagers[0].Id }
            };
        await _dbContext.Events.AddRangeAsync(events);
        await _dbContext.SaveChangesAsync();

        // Seed Amenities
        var amenities = new List<Amenity>
        {
            new Amenity { Id = Guid.NewGuid(), Name = "Wi-Fi" },
            new Amenity { Id = Guid.NewGuid(), Name = "Pool" },
            new Amenity { Id = Guid.NewGuid(), Name = "Gym" }
        };
        await _dbContext.Amenities.AddRangeAsync(amenities);
        await _dbContext.SaveChangesAsync();

        // Seed RoomTypes
        var roomTypes = new List<RoomType>
        {
            new RoomType { Id = Guid.NewGuid(), Description = "Standard" },
            new RoomType { Id = Guid.NewGuid(), Description = "Deluxe" },
            new RoomType { Id = Guid.NewGuid(), Description = "Suite" }
        };
        await _dbContext.RoomTypes.AddRangeAsync(roomTypes);
        await _dbContext.SaveChangesAsync();

        // Seed Rooms
        var rooms = new List<Room>();
        foreach (var hotel in hotels)
        {
            for (int i = 1; i <= 5; i++)
            {
                rooms.Add(new Room
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = i,
                    MaxOccupancy = 2,
                    Description = $"A nice room in {hotel.Name}",
                    Price = 100 + (i * 10),
                    HotelId = hotel.Id,
                    RoomTypeId = roomTypes[i % roomTypes.Count].Id
                });
            }
        }
        await _dbContext.Rooms.AddRangeAsync(rooms);
        await _dbContext.SaveChangesAsync();

        // Update hotel room count
        foreach (var hotel in hotels)
        {
            hotel.NumberOfRooms = rooms.Count(r => r.HotelId == hotel.Id);
        }
        _dbContext.Hotels.UpdateRange(hotels);
        await _dbContext.SaveChangesAsync();

        // Seed RoomAmenities
        var roomAmenities = new List<RoomAmenity>();
        foreach (var room in rooms)
        {
            roomAmenities.Add(new RoomAmenity { Id = Guid.NewGuid(), RoomId = room.Id, AmenityId = amenities[random.Next(amenities.Count)].Id });
        }
        await _dbContext.RoomAmenities.AddRangeAsync(roomAmenities);
        await _dbContext.SaveChangesAsync();

        // Seed HotelReservations
        var hotelReservations = new List<HotelReservation>
        {
            new HotelReservation { Id = Guid.NewGuid(), ReservationDateTime = DateTime.UtcNow, ReceivationStartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)), ReceivationEndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(15)), NumberOfPeople = 2, HotelId = hotels[0].Id, UserId = users[0].Id, RoomId = rooms[0].Id },
            new HotelReservation { Id = Guid.NewGuid(), ReservationDateTime = DateTime.UtcNow, ReceivationStartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20)), ReceivationEndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(25)), NumberOfPeople = 1, HotelId = hotels[1].Id, UserId = users[1].Id, RoomId = rooms[5].Id }
        };
        await _dbContext.HotelReservations.AddRangeAsync(hotelReservations);
        await _dbContext.SaveChangesAsync();

        // Seed HotelReviews
        var hotelReviews = new List<HotelReview>
        {
            new HotelReview { Id = Guid.NewGuid(), HotelId = hotels[0].Id, UserId = users[0].Id, ReviewDateTime = DateTime.UtcNow, Description = "Great hotel!", OverallRating = 5, ServiceRating = 5, CleanlinessRating = 5, ValueRating = 5 },
            new HotelReview { Id = Guid.NewGuid(), HotelId = hotels[1].Id, UserId = users[1].Id, ReviewDateTime = DateTime.UtcNow, Description = "Nice place.", OverallRating = 4, ServiceRating = 4, CleanlinessRating = 4, ValueRating = 4 }
        };
        await _dbContext.HotelReviews.AddRangeAsync(hotelReviews);
        await _dbContext.SaveChangesAsync();

        // Seed Cuisines
        var cuisines = new List<Cuisine>
        {
            new Cuisine { Id = Guid.NewGuid(), Name = "Italian" },
            new Cuisine { Id = Guid.NewGuid(), Name = "Mexican" }
        };
        await _dbContext.Cuisines.AddRangeAsync(cuisines);
        await _dbContext.SaveChangesAsync();

        // Seed Dishes
        var dishes = new List<Dish>
        {
            new Dish { Id = Guid.NewGuid(), Name = "Pizza" },
            new Dish { Id = Guid.NewGuid(), Name = "Tacos" }
        };
        await _dbContext.Dishes.AddRangeAsync(dishes);
        await _dbContext.SaveChangesAsync();

        // Seed Features
        var features = new List<Feature>
        {
            new Feature { Id = Guid.NewGuid(), Name = "Outdoor Seating" },
            new Feature { Id = Guid.NewGuid(), Name = "Live Music" }
        };
        await _dbContext.Features.AddRangeAsync(features);
        await _dbContext.SaveChangesAsync();

        // Seed MealTypes
        var mealTypes = new List<MealType>
        {
            new MealType { Id = Guid.NewGuid(), Name = "Breakfast" },
            new MealType { Id = Guid.NewGuid(), Name = "Lunch" },
            new MealType { Id = Guid.NewGuid(), Name = "Dinner" }
        };
        await _dbContext.MealTypes.AddRangeAsync(mealTypes);
        await _dbContext.SaveChangesAsync();

        // Seed Tags
        var tags = new List<Tag>
        {
            new Tag { Id = Guid.NewGuid(), Name = "Casual" },
            new Tag { Id = Guid.NewGuid(), Name = "Fine Dining" }
        };
        await _dbContext.Tags.AddRangeAsync(tags);
        await _dbContext.SaveChangesAsync();

        // Seed WorkTimes
        var workTimes = new List<WorkTime>
        {
            new WorkTime { Id = Guid.NewGuid(), Day = DayOfWeek.Monday, OpenHour = new TimeOnly(9, 0), CloseHour = new TimeOnly(22, 0) },
            new WorkTime { Id = Guid.NewGuid(), Day = DayOfWeek.Tuesday, OpenHour = new TimeOnly(9, 0), CloseHour = new TimeOnly(22, 0) },
        };
        await _dbContext.WorkTimes.AddRangeAsync(workTimes);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantCuisines
        var restaurantCuisines = new List<RestaurantCuisine>
        {
            new RestaurantCuisine { Id = Guid.NewGuid(), RestaurantId = restaurants[0].Id, CuisineId = cuisines[0].Id },
            new RestaurantCuisine { Id = Guid.NewGuid(), RestaurantId = restaurants[1].Id, CuisineId = cuisines[1].Id }
        };
        await _dbContext.RestaurantCuisines.AddRangeAsync(restaurantCuisines);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantDishPrices
        var restaurantDishPrices = new List<RestaurantDishPrice>
        {
            new RestaurantDishPrice { Id = Guid.NewGuid(), RestaurantId = restaurants[0].Id, DishId = dishes[0].Id, Price = 20 },
            new RestaurantDishPrice { Id = Guid.NewGuid(), RestaurantId = restaurants[1].Id, DishId = dishes[1].Id, Price = 10 }
        };
        await _dbContext.RestaurantDishPrices.AddRangeAsync(restaurantDishPrices);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantFeatures
        var restaurantFeatures = new List<RestaurantFeature>
        {
            new RestaurantFeature { Id = Guid.NewGuid(), RestaurantId = restaurants[0].Id, FeatureId = features[0].Id },
            new RestaurantFeature { Id = Guid.NewGuid(), RestaurantId = restaurants[1].Id, FeatureId = features[1].Id }
        };
        await _dbContext.RestaurantFeatures.AddRangeAsync(restaurantFeatures);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantMealTypes
        var restaurantMealTypes = new List<RestaurantMealType>
        {
            new RestaurantMealType { Id = Guid.NewGuid(), RestaurantId = restaurants[0].Id, MealTypeId = mealTypes[2].Id },
            new RestaurantMealType { Id = Guid.NewGuid(), RestaurantId = restaurants[1].Id, MealTypeId = mealTypes[1].Id }
        };
        await _dbContext.RestaurantMealTypes.AddRangeAsync(restaurantMealTypes);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantTags
        var restaurantTags = new List<RestaurantTag>
        {
            new RestaurantTag { Id = Guid.NewGuid(), RestaurantId = restaurants[0].Id, TagId = tags[1].Id },
            new RestaurantTag { Id = Guid.NewGuid(), RestaurantId = restaurants[1].Id, TagId = tags[0].Id }
        };
        await _dbContext.RestaurantTags.AddRangeAsync(restaurantTags);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantWorkTimes
        var restaurantWorkTimes = new List<RestaurantWorkTime>
        {
            new RestaurantWorkTime { Id = Guid.NewGuid(), RestaurantId = restaurants[0].Id, WorkTimeId = workTimes[0].Id },
            new RestaurantWorkTime { Id = Guid.NewGuid(), RestaurantId = restaurants[1].Id, WorkTimeId = workTimes[1].Id }
        };
        await _dbContext.RestaurantWorkTimes.AddRangeAsync(restaurantWorkTimes);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantReviews
        var restaurantReviews = new List<RestaurantReview>
        {
            new RestaurantReview { Id = Guid.NewGuid(), RestaurantId = restaurants[0].Id, UserId = users[0].Id, ReviewDateTime = DateTime.UtcNow, Description = "Amazing pizza!", CustomerStarRating = 5, CustomerServiceStarRating = 5, CustomerFoodStarRating = 5 },
            new RestaurantReview { Id = Guid.NewGuid(), RestaurantId = restaurants[1].Id, UserId = users[1].Id, ReviewDateTime = DateTime.UtcNow, Description = "Decent tacos.", CustomerStarRating = 4, CustomerServiceStarRating = 4, CustomerFoodStarRating = 4 }
        };
        await _dbContext.RestaurantReviews.AddRangeAsync(restaurantReviews);
        await _dbContext.SaveChangesAsync();

        // Seed RestaurantBookings
        var restaurantBookings = new List<RestaurantBooking>
        {
            new RestaurantBooking { Id = Guid.NewGuid(), BookingDateTime = DateTime.UtcNow, ReceiveDateTime = DateTime.UtcNow.AddHours(2), BookingDurationTime = new TimeOnly(1, 0, 0), NumberOfPeople = 2, TableNumber = 5, RestaurantId = restaurants[0].Id, UserId = users[0].Id },
            new RestaurantBooking { Id = Guid.NewGuid(), BookingDateTime = DateTime.UtcNow, ReceiveDateTime = DateTime.UtcNow.AddHours(3), BookingDurationTime = new TimeOnly(0, 45, 0), NumberOfPeople = 4, TableNumber = 2, RestaurantId = restaurants[1].Id, UserId = users[1].Id }
        };
        await _dbContext.RestaurantBookings.AddRangeAsync(restaurantBookings);
        await _dbContext.SaveChangesAsync();

        // Seed BookingDishes
        var bookingDishes = new List<BookingDish>
        {
            new BookingDish { Id = Guid.NewGuid(), RestaurantBookingId = restaurantBookings[0].Id, DishId = dishes[0].Id, Quantity = 1 },
            new BookingDish { Id = Guid.NewGuid(), RestaurantBookingId = restaurantBookings[1].Id, DishId = dishes[1].Id, Quantity = 2 }
        };
        await _dbContext.BookingDishes.AddRangeAsync(bookingDishes);
        await _dbContext.SaveChangesAsync();

        // Seed EventRegistrations
        var eventRegistrations = new List<EventRegistration>
        {
            new EventRegistration { Id = Guid.NewGuid(), RegistrationDateTime = DateTime.UtcNow, NumberOfPeople = 2, CustomerId = users[0].Id, EventId = events[0].Id },
            new EventRegistration { Id = Guid.NewGuid(), RegistrationDateTime = DateTime.UtcNow, NumberOfPeople = 1, CustomerId = users[1].Id, EventId = events[1].Id }
        };
        await _dbContext.EventRegistrations.AddRangeAsync(eventRegistrations);
        await _dbContext.SaveChangesAsync();

        // Seed EventReviews
        var eventReviews = new List<EventReview>
        {
            new EventReview { Id = Guid.NewGuid(), EventId = events[0].Id, CustomerId = users[0].Id, ReviewDateTime = DateTime.UtcNow, Description = "Awesome festival!", Rating = 5 },
            new EventReview { Id = Guid.NewGuid(), EventId = events[1].Id, CustomerId = users[1].Id, ReviewDateTime = DateTime.UtcNow, Description = "Great food!", Rating = 4 }
        };
        await _dbContext.EventReviews.AddRangeAsync(eventReviews);
        await _dbContext.SaveChangesAsync();
    }
}