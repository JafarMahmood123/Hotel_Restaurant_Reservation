using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HotelRestaurantDbContext>
{
    public HotelRestaurantDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelRestaurantDbContext>();
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-NK8UNAQ;Initial Catalog=HotelRestaurantReservation;Integrated Security=True;Trust Server Certificate=True");

        return new HotelRestaurantDbContext(optionsBuilder.Options);
    }
}
