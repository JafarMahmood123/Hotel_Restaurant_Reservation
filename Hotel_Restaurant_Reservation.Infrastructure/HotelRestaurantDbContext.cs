using Hotel_Restaurant_Reservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Infrastructure;

public class HotelRestaurantDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<RoomAmenity> RoomAmenities { get; set; }

    public DbSet<RoomType> RoomTypes { get; set; }

    public DbSet<HotelRangePrices> HotelRangePrices { get; set; }

    public DbSet<PropertyType> PropertyTypes { get; set; }

    public DbSet<CurrencyType> CurrencyTypes { get; set; }

    public DbSet<HotelReservation> HotelReservations { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<LocalLocation> LocalLocations { get; set; }

    public DbSet<Location> Locations { get; set; }


}
