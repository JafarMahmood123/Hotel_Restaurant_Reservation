namespace Hotel_Restaurant_Reservation.Application.Implementation.Locations.Queries;

public class LocationResponse
{
    public Guid Id { get; set; }

    public Guid CountryId { get; set; }

    public Guid CityId { get; set; }

    public Guid LocalLocationId { get; set; }
}
