namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.AddLocation;

public class AddLocationRequest
{
    public Guid CountryId { get; set; }
    public Guid CityLocalLocationsId { get; set; }
}