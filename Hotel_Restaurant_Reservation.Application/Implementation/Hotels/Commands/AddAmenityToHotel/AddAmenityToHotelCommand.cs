using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAmenitiesByHotelId;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddAmenityToHotel;

public class AddAmenityToHotelCommand : ICommand<Result<GetAmenitiesByHotelIdResponse>>
{
    public AddAmenityToHotelCommand(AddAmenityToHotelRequest addAmenityToHotelRequest,
        Guid hotelId, Guid amenityId)
    {
        AddAmenityToHotelRequest = addAmenityToHotelRequest;
        HotelId = hotelId;
        AmenityId = amenityId;
    }

    public AddAmenityToHotelRequest AddAmenityToHotelRequest { get; }
    public Guid HotelId { get; }
    public Guid AmenityId { get; }
}
