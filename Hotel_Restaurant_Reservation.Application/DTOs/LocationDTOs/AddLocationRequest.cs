using Hotel_Restaurant_Reservation.Application.DTOs.CityDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.CountryDTOs;
using Hotel_Restaurant_Reservation.Application.DTOs.LocalLocationDTOs;

namespace Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;

public class AddLocationRequest
{
    public Guid CountryId { get; set; }

    public Guid CityId { get; set; }

    public Guid LocalLocationId { get; set; }
}
