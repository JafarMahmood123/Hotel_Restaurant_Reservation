using CsvHelper;
using Hotel_Restaurant_Reservation.Domain.Entities;
using System.Globalization;

namespace Hotel_Restaurant_Reservation.Seed;

internal static class RestaurantSeeder
{
    public static string Path;

    public static void Insert()
    {
        if (Path is null)
            throw new Exception("PAth to the files is null.");

        using (var reader = new StreamReader(Path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<dynamic>();

            foreach (var record in records)
            {

                Restaurant hotel = new Restaurant();

                Country country = new Country();
                City city = new City();
                Location location = new Location();
                LocalLocation localLocation = new LocalLocation();

                RestaurantRangePrices currencyType = new RestaurantRangePrices();

                PriceLevel propertyType = new PriceLevel();

                List<WorkTime> workTimes = new List<WorkTime>();

                List<Feature> features = new List<Feature>();

                List<Tag> tags = new List<Tag>();

                List<Cuisine> cuisines = new List<Cuisine>();

                List<Dish> dishes = new List<Dish>();

                List<MealType> mealsTypes = new List<MealType>();



                try
                {
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }

}
