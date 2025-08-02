using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.RemoveAmenityFromHotel;

public class RemoveAmenityFromHotelCommand : ICommand<Result>
{
    public RemoveAmenityFromHotelCommand(Guid hotelId, Guid amenityId)
    {
        HotelId = hotelId;
        AmenityId = amenityId;
    }

    public Guid HotelId { get; }
    public Guid AmenityId { get; }
}