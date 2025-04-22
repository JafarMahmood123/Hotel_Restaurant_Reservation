using CsvHelper;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

DateTime t1 = new DateTime();

string path = "D:\\Project\\HotelDataSet\\";

string fileName = "Hotel_details.csv";

string pathToFile = path + fileName;

int counter = 0;

var options = new DbContextOptionsBuilder<HotelRestaurantDbContext>()
    .UseSqlServer("Data Source=DESKTOP-NK8UNAQ;Initial Catalog=HotelRestaurantReservation;Integrated Security=True;Trust Server Certificate=True")
    .Options;
HotelRestaurantDbContext hotelRestaurantDbContext = new HotelRestaurantDbContext(options);


using (var reader = new StreamReader(pathToFile))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<dynamic>();

    foreach (var record in records)
    {
        //counter++;

        //Hotel hotel = new Hotel();
        
        //Location location = new Location();
        //Country country = new Country();
        //City city = new City();
        //LocalLocation localLocation = new LocalLocation();
        
        //CurrencyType currencyType = new CurrencyType();

        //PropertyType propertyType = new PropertyType();

        try
        {
            //propertyType.Id = counter;
            //propertyType.Name = record.propertytype;

            //country.Id = counter;
            //country.Name = record.country;

            //city.Id = counter;
            //city.Name = record.city;

            //localLocation.Id = counter;
            //localLocation.Name = record.address;
            

            //location.Id = counter;
            //location.CountryId = country.Id;
            //location.CityId = city.Id;
            //location.LocalLocationId = localLocation.Id;

            //currencyType.CurrencyCode = record.curr;

            //hotel.Id = int.Parse(record.hotelid);
            //hotel.Name = record.hotelname;
            //hotel.StarRate = double.Parse(record.starrating);
            //hotel.Latitude = double.Parse(record.latitude);
            //hotel.Longitude = double.Parse(record.longitude);
            //hotel.Url = record.url;
            //hotel.PropertyTypeId = propertyType.Id;
            //hotel.LocationId = location.Id;
            //hotel.CurrencyTypeId = currencyType.Id;

            ////hotels.Add(hotel);
            

            //hotelRestaurantDbContext.Countries.Add(country);
            //hotelRestaurantDbContext.Cities.Add(city);
            //hotelRestaurantDbContext.LocalLocations.Add(localLocation);
            //hotelRestaurantDbContext.Locations.Add(location);
            //hotelRestaurantDbContext.PropertyTypes.Add(propertyType);
            //hotelRestaurantDbContext.CurrencyTypes.Add(currencyType);
            //hotelRestaurantDbContext.Hotels.Add(hotel);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}


DateTime t2 = new DateTime();

Console.WriteLine($"Time Consumed To Convert = {t2 - t1}.");