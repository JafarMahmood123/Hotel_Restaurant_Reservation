using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Seeds.Hotels;

public class CreateHotelSeedCommand : ICommand<Hotel>
{
    public Hotel Hotel { get; set; }

    public CreateHotelSeedCommand(Hotel hotel)
    {
        Hotel = hotel;
    }
}
