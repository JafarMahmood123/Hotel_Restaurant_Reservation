using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;

public class AddHotelCommand : ICommand<Result<HotelResponse>>
{
    public AddHotelRequest AddHotelRequest { get; set; }

    public AddHotelCommand(AddHotelRequest addHotelRequest)
    {
        AddHotelRequest = addHotelRequest;
    }
}