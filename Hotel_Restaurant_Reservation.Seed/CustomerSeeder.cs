using CsvHelper;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure;
using Hotel_Restaurant_Reservation.Seed.Fields;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

namespace Hotel_Restaurant_Reservation.Seed;

public static class CustomerSeeder
{
    public static string Path;

    private static HotelRestaurantDbContext hotelRestaurantDbContext = new DesignTimeDbContextFactory().
        CreateDbContext([]);

    private static int recordNumber = 0;

    private static int NumberOfErrors = 0;
    public async static void Insert()
    {

        if (Path is null)
            throw new Exception("Path to the files is null.");

        using (var reader = new StreamReader(Path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {

            var records = csv.GetRecords<dynamic>();

            foreach (var record in records)
            {
                recordNumber++;

                Customer customer = new Customer();

                Role role = new Role();

                Country country = new Country();
                City city = new City();
                LocalLocation localLocation = new LocalLocation();

                Location location = new Location();

                CityLocalLocations cityLocalLocations = new CityLocalLocations();


                try
                {
                    GenerateCountry(record, country);
                    GenerateCity(record, country, city);
                    GenerateLocalLocation(record, localLocation);
                    GenerateCityLocalLocation(city, localLocation, cityLocalLocations);
                    GenerateLocation(country, location, cityLocalLocations);
                    GenerateCustomer(record, customer, location);
                    GenerateRole(record, role);
                    GenerateCustomerRoles(customer, role);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("##################################################################");
                    Console.WriteLine("Error at record " + recordNumber);
                    NumberOfErrors++;
                    Console.WriteLine(ex.Message);
                }
            }
        }

        Console.WriteLine("The number of total errors = " + NumberOfErrors);
    }

    private static void GenerateCustomerRoles(Customer customer, Role role)
    {
        var customerRoles = new CustomerRoles();
        var existingCustomerRole = hotelRestaurantDbContext.CustomersRoles.FirstOrDefault(x => x.RoleId == role.Id
        && x.CustomerId == customer.Id);
        if (existingCustomerRole != null)
        {
            customerRoles.Id = existingCustomerRole.Id;
            customerRoles.RoleId = existingCustomerRole.RoleId;
            customerRoles.CustomerId = existingCustomerRole.Id;
            return;
        }

        customerRoles.Id = Guid.NewGuid();
        customerRoles.RoleId = role.Id;
        customerRoles.CustomerId = customer.Id;
        hotelRestaurantDbContext.CustomersRoles.Add(customerRoles);


        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateRole(dynamic record, Role role)
    {
        string roleName = record.role;
        var existingRole = hotelRestaurantDbContext.Roles.FirstOrDefault(x => x.Name == roleName);
        if (existingRole != null)
        {
            role.Id = existingRole.Id;
            role.Name = existingRole.Name;

            return;
        }

        role.Id = Guid.NewGuid();
        role.Name = roleName;

        hotelRestaurantDbContext.Roles.Add(role);


        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateCustomer(dynamic record, Customer customer, Location location)
    {
        Guid customerId = Guid.NewGuid();
        string customerFirstName = record.first_name;
        string customerLastName = record.last_name;
        DateOnly customerBirthDate = DateOnly.Parse(record.birthdate);
        int customerAge = int.Parse(record.age);
        string customerEmail = record.email;
        string customerPassword = record.password;

        var existingCustomer = hotelRestaurantDbContext.Customers.FirstOrDefault(x => x.Id == customerId);
        if (existingCustomer != null)
        {
            customer.Id = existingCustomer.Id;
            customer.FirstName = existingCustomer.FirstName;
            customer.LastName = existingCustomer.LastName;
            customer.BirthDate = existingCustomer.BirthDate;
            customer.Age = existingCustomer.Age;
            customer.Email = existingCustomer.Email;
            customer.Password = existingCustomer.Password;
            customer.LocationId = location.Id;

            return;
        }

        customer.Id = Guid.NewGuid();
        customer.FirstName = customerFirstName;
        customer.LastName = customerLastName;
        customer.BirthDate = customerBirthDate;
        customer.Age = customerAge;
        customer.Email = customerEmail;
        customer.Password = customerPassword;
        customer.LocationId = location.Id;

        hotelRestaurantDbContext.Customers.Add(customer);


        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateLocation(Country country, Location location, CityLocalLocations cityLocalLocations)
    {
        var existingLocation = hotelRestaurantDbContext.Locations.FirstOrDefault(x => x.CountryId == country.Id
                && x.CityLocalLocationsId == cityLocalLocations.Id);

        if (existingLocation is not null)
        {
            location.Id = existingLocation.Id;
            location.CountryId = existingLocation.CountryId;
            location.CityLocalLocationsId = cityLocalLocations.Id;
            return;
        }

        location.Id = Guid.NewGuid();
        location.CountryId = country.Id;
        location.CityLocalLocationsId = cityLocalLocations.Id;

        hotelRestaurantDbContext.Add(location);
        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateCityLocalLocation(City city, LocalLocation localLocation, CityLocalLocations cityLocalLocations)
    {
        var existingCityLocalLocation = hotelRestaurantDbContext.CityLocalLocations.FirstOrDefault(x => x.CityId == city.Id
                            && x.LocalLocationId == localLocation.Id);

        if (existingCityLocalLocation is not null)
        {
            cityLocalLocations.Id = existingCityLocalLocation.Id;
            cityLocalLocations.CityId = existingCityLocalLocation.CityId;
            cityLocalLocations.LocalLocationId = existingCityLocalLocation.LocalLocationId;
            return;
        }


        cityLocalLocations.Id = Guid.NewGuid();
        cityLocalLocations.CityId = city.Id;
        cityLocalLocations.LocalLocationId = localLocation.Id;

        hotelRestaurantDbContext.CityLocalLocations.Add(cityLocalLocations);

        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateLocalLocation(dynamic record, LocalLocation localLocation)
    {
        string localLocationName = record.local_address;

        var existingLocalLocation = hotelRestaurantDbContext.LocalLocations.FirstOrDefault(x => x.Name == localLocationName);
        if (existingLocalLocation is not null)
        {
            localLocation.Id = existingLocalLocation.Id;
            localLocation.Name = existingLocalLocation.Name;
            return;
        }


        localLocation.Id = Guid.NewGuid();
        localLocation.Name = localLocationName;

        hotelRestaurantDbContext.LocalLocations.Add(localLocation);
        hotelRestaurantDbContext.SaveChanges();

    }

    private static void GenerateCity(dynamic record, Country country, City city)
    {
        string cityName = record.city;

        var existingCity = hotelRestaurantDbContext.Cities.FirstOrDefault(x => x.Name == cityName);
        if (existingCity is not null)
        {
            city.Id = existingCity.Id;
            city.Name = existingCity.Name;
            city.CountryId = country.Id;
            return;
        }

        city.Id = Guid.NewGuid();
        city.Name = cityName;
        city.CountryId = country.Id;

        hotelRestaurantDbContext.Cities.Add(city);

        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }

    private static void GenerateCountry(dynamic record, Country country)
    {
        string countryName = record.country;

        var existingCountry = hotelRestaurantDbContext.Countries.FirstOrDefault(x => x.Name == countryName);
        if (existingCountry is not null)
        {
            country.Id = existingCountry.Id;
            countryName = existingCountry.Name;
            return;
        }

        country.Id = Guid.NewGuid();
        country.Name = countryName;
        hotelRestaurantDbContext.Countries.Add(country);


        try
        {
            hotelRestaurantDbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            Console.WriteLine("##################################################################");
            Console.WriteLine("Error at record " + recordNumber);
            Console.WriteLine($"SQL Error Number: {sqlEx.Number}");
            Console.WriteLine($"Error Message: {sqlEx.Message}");
            Console.WriteLine($"Line Number: {sqlEx.LineNumber}");
        }
    }
}
