namespace Hotel_Restaurant_Reservation.Application.DTOs.LocationDTOs;

public class AddLocationRequest
{
    public Guid CountryId { get; set; }

    public Guid CityLocalLocationsId { get; set; }
}
