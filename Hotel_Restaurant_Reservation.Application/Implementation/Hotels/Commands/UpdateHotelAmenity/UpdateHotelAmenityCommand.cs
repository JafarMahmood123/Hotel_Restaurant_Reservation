using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotelAmenity;

public class UpdateHotelAmenityCommand : ICommand<Result>
{
    public UpdateHotelAmenityCommand(Guid HotelId, Guid AmenityId, double newPrice)
    {
        this.HotelId = HotelId;
        this.AmenityId = AmenityId;
        NewPrice = newPrice;
    }

    public Guid HotelId { get; }
    public Guid AmenityId { get; }
    public double NewPrice { get; }
}
