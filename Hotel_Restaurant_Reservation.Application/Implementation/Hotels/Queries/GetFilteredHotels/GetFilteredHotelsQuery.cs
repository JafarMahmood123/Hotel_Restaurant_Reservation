using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetFilteredHotels
{
    public class GetFilteredHotelsQuery : IQuery<Result<IEnumerable<HotelResponse>>>
    {
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? LocalLocationId { get; set; }
        public Guid? PropertyTypeId { get; set; }
        public Guid? AmenityId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public double? MinStarRate { get; set; }
        public double? MaxStarRate { get; set; }
    }
}
