using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HotelRestaurantDbContext>
{
    public HotelRestaurantDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelRestaurantDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=HotelRestaurantReservation;User=sa;Password=Jafar123!;TrustServerCertificate=True;");

        return new HotelRestaurantDbContext(optionsBuilder.Options);
    }
}
