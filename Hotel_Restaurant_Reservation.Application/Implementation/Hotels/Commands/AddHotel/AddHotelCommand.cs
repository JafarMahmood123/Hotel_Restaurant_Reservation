using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;

public class AddHotelCommand : ICommand<Hotel>
{
    public Hotel Hotel { get; set; }

    public AddHotelCommand(Hotel hotel)
    {
        Hotel = hotel;
    }
}
