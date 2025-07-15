using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Queries.GetAllHotelReviewsByHotelId
{
    public class GetAllHotelReviewsByHotelIdQuery : IQuery<Result<IEnumerable<HotelReviewResponse>>>
    {
        public GetAllHotelReviewsByHotelIdQuery(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid HotelId { get; }
    }
}