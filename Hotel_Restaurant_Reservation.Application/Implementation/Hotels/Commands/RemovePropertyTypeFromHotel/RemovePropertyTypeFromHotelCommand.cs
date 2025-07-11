using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.RemovePropertyTypeFromHotel;

public class RemovePropertyTypeFromHotelCommand : ICommand<Result>
{
    public RemovePropertyTypeFromHotelCommand(Guid hotelId)
    {
        HotelId = hotelId;
    }

    public Guid HotelId { get; }
}