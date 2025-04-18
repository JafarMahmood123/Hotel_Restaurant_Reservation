using Hotel_Restaurant_Reservation.Application.DTOs.City;
using Hotel_Restaurant_Reservation.Application.DTOs.Country;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocation;

namespace Hotel_Restaurant_Reservation.Application.DTOs.Location;

public class LocationRequest
{
    public CountryRequest CountryRequest { get; set; }

    public CityRequest CityRequest { get; set; }

    public LocalLocationRequest LocalLocationRequest { get; set; }
}
