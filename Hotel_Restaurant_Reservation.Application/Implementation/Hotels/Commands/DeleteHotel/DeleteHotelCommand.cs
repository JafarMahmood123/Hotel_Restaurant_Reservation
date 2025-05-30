using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommand : ICommand<HotelResponse?>
{
    public DeleteHotelCommand(Guid hotelId)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; }
}
