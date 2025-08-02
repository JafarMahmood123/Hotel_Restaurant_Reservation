namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Commands.CheckExistingLocationWithoutLocalLocation;

public class CheckExistingLocationWithoutLocalLocationRequest
{
    public Guid CountryId { get; set; }

    public Guid CityId { get; set; }

    public Guid? LocalLocationId { get; set; }
}
