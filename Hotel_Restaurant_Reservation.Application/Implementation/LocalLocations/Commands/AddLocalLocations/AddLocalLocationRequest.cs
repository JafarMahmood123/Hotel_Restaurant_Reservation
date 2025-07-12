namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;

public class AddLocalLocationRequest
{
    public string Name { get; set; }

    public Guid CityId { get; set; }

}
