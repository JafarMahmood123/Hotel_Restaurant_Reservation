// Program.cs
// This is the corrected version that uses your specific DbContext and connection string names.

// Make sure you have the correct using statements at the top.
using Hotel_Restaurant_Reservation.Infrastructure; // Assuming your DbContext is in this namespace
using Hotel_Restaurant_Reservation.Seed;         // Your seeder's namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

/// <summary>
/// This is the main entry point for your console application.
/// It sets up the database connection, configuration, and then
/// runs the YelpDataSeeder to populate your database.
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        // --- 1. Set up configuration to read from appsettings.json ---
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // --- 2. Set up the dependency injection container ---
        var services = new ServiceCollection();

        // Configure the DbContext to use your specific class and connection string name
        services.AddDbContext<HotelRestaurantDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("HotelRestaurantReservation")));

        var serviceProvider = services.BuildServiceProvider();

        Console.WriteLine("Database seeding process initiated...");

        // --- 3. Create a 'scope' to resolve services ---
        using (var scope = serviceProvider.CreateScope())
        {
            // Resolve your specific DbContext
            var dbContext = scope.ServiceProvider.GetRequiredService<HotelRestaurantDbContext>();

            // Apply any pending migrations to ensure the DB schema is up to date.
            Console.WriteLine("Applying database migrations...");
            await dbContext.Database.MigrateAsync();
            Console.WriteLine("Migrations applied successfully.");

            // --- 4. Define the paths to your Yelp dataset files ---
            // !!! IMPORTANT: YOU MUST UPDATE THESE PATHS IF THEY ARE INCORRECT !!!
            string businessFilePath = @"D:\Yelp-JSON\Yelp JSON\yelp_dataset\yelp_dataset~\yelp_academic_dataset_business.json";
            string reviewFilePath = @"D:\Yelp-JSON\Yelp JSON\yelp_dataset\yelp_dataset~\yelp_academic_dataset_review.json";

            // --- 5. Instantiate the seeder and run the seeding methods ---
            var seeder = new YelpDataSeeder(dbContext);

            // First, seed the restaurants and their mappings.
            await seeder.SeedRestaurantsAsync(businessFilePath, maxRecords: 1000);

            // Second, seed the users and reviews, which depend on the restaurants.
            await seeder.SeedReviewsAsync(reviewFilePath, maxRecords: 5000);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nData seeding process completed successfully!");
        Console.ResetColor();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}
