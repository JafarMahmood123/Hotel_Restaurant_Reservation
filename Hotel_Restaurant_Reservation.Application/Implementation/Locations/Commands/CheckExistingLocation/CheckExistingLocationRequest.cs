namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.CheckExistingLocation;

public class CheckExistingLocationRequest
{
    public Guid CountryId { get; set; }

    public Guid CityId { get; set; }

    public Guid LocalLocationId { get; set; }
}
