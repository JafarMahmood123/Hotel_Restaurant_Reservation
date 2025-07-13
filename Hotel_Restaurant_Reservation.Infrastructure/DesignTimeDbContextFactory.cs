using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HotelRestaurantDbContext>
{
    public HotelRestaurantDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HotelRestaurantDbContext>();
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-TVPDTBP;Database=HotelRestaurantReservation;Integrated Security=True;Trust Server Certificate=True");

        return new HotelRestaurantDbContext(optionsBuilder.Options);
    }
}

//Server=localhost,1433;Database=HotelRestaurantReservation;User=sa;Password=Jafar123!;TrustServerCertificate=True;