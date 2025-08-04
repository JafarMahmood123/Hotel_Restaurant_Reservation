using Hotel_Restaurant_Reservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Infrastructure;

public class HotelRestaurantDbContext : DbContext
{
    public HotelRestaurantDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomImage> RoomImages { get; set; }
    public DbSet<RoomAmenity> RoomAmenities { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<PropertyType> PropertyTypes { get; set; }
    public DbSet<CurrencyType> CurrencyTypes { get; set; }
    public DbSet<HotelReservation> HotelReservations { get; set; }
    public DbSet<RestaurantReview> RestaurantReviews { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<LocalLocation> LocalLocations { get; set; }
    public DbSet<CityLocalLocations> CityLocalLocations { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<RestaurantCuisine> RestaurantCuisines { get; set; }
    public DbSet<RestaurantFeature> RestaurantFeatures { get; set; }
    public DbSet<RestaurantMealType> RestaurantMealTypes { get; set; }
    public DbSet<RestaurantDish> RestaurantDishPrices { get; set; }
    public DbSet<RestaurantTag> RestaurantTags { get; set; }
    public DbSet<RestaurantWorkTime> RestaurantWorkTimes { get; set; }
    public DbSet<RestaurantCurrencyType> RestaurantCurrencyTypes { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Cuisine> Cuisines { get; set; }
    public DbSet<MealType> MealTypes { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventRegistration> EventRegistrations { get; set; }
    public DbSet<RestaurantBooking> RestaurantBookings { get; set; }
    public DbSet<BookingDish> BookingDishes { get; set; }
    public DbSet<EventReview> EventReviews { get; set; }
    public DbSet<HotelAmenityPrice> HotelAmenityPrices { get; set; }
    public DbSet<HotelCurrencyType> HotelCurrencyTypes { get; set; }
    public DbSet<HotelReview> HotelReviews { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<HotelReservationPayment> HotelReservationPayments { get; set; }
    public DbSet<RestaurantBookingPayment> RestaurantBookingPayments { get; set; }
    public DbSet<EventRegistrationPayment> EventRegistrationPayments { get; set; }
    public DbSet<EventCurrencyType> EventCurrencyTypes { get; set; }
    public DbSet<EventImage> EventImages { get; set; }
    public DbSet<HotelImage> HotelImages { get; set; }
    public DbSet<RestaurantImage> RestaurantImages { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
}
