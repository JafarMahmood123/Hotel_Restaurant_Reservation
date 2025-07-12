using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommand : ICommand<HotelResponse?>
{
    public DeleteHotelCommand(Guid hotelId)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; }
}
