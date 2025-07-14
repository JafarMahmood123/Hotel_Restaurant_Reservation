namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.UpdateLocation;

public class UpdateLocationRequest
{
    public Guid CountryId { get; set; }
    public Guid CityLocalLocationsId { get; set; }
}