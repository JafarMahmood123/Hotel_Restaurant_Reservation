using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;

public class AddHotelCommand : ICommand<Hotel>
{
    public HotelAddRequest HotelAddRequest { get; set; }

    public AddHotelCommand(HotelAddRequest hotelAddRequest)
    {
        HotelAddRequest = hotelAddRequest;
    }
}
