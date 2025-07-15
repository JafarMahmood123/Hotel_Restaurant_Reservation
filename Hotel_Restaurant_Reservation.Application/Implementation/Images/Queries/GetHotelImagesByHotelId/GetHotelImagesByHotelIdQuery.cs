using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetHotelImagesByHotelId
{
    public class GetHotelImagesByHotelIdQuery : IQuery<Result<List<string>>>
    {
        public GetHotelImagesByHotelIdQuery(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid HotelId { get; }
    }
}