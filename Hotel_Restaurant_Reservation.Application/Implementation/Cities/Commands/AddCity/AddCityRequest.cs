namespace Hotel_Restaurant_Reservation.Application.Implementation.Cities.Commands.AddCity;

public class AddCityRequest
{
    public string Name { get; set; }
    public Guid CountryId { get; set; }
}