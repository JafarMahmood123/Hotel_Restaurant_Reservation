namespace Hotel_Restaurant_Reservation.Application.DTOs.CityDTOs;

public class AddCityRequest
{
    public string Name { get; set; }

    public Guid CountryId { get; set; }
}
