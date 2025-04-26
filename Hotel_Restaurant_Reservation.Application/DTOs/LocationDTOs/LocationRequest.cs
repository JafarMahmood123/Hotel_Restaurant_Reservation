using Hotel_Restaurant_Reservation.Application.DTOs.CityDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;

namespace Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;

public class LocationRequest
{
    public CountryRequest CountryRequest { get; set; }

    public AddCityRequest CityRequest { get; set; }

    public LocalLocationRequest LocalLocationRequest { get; set; }
}
