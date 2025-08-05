using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels
{
    /// <summary>
    /// A query to retrieve a paginated and filtered list of hotels.
    /// </summary>
    public class GetAllHotelsQuery : IQuery<Result<PagedResult<HotelResponse>>>
    {
        public GetAllHotelsQuery(
            int page = 1,
            int pageSize = 10,
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? localLocationId = null,
            Guid? propertyTypeId = null,
            Guid? amenityId = null,
            double? minPrice = 0,
            double? maxPrice = double.MaxValue,
            double? minStarRate = 0,
            double? maxStarRate = 5)
        {
            // CORRECTED: Use the 'this' keyword to refer to the class properties.
            this.Page = page;
            this.PageSize = pageSize;
            this.CountryId = countryId;
            this.CityId = cityId;
            this.LocalLocationId = localLocationId;
            this.PropertyTypeId = propertyTypeId;
            this.AmenityId = amenityId;
            this.MinPrice = minPrice;
            this.MaxPrice = maxPrice;
            this.MinStarRate = minStarRate;
            this.MaxStarRate = maxStarRate;
        }

        // Pagination Properties
        public int Page { get; }
        public int PageSize { get; }

        // Filter Properties
        public Guid? CountryId { get; }
        public Guid? CityId { get; }
        public Guid? LocalLocationId { get; }
        public Guid? PropertyTypeId { get; }
        public Guid? AmenityId { get; }
        public double? MinPrice { get; }
        public double? MaxPrice { get; }
        public double? MinStarRate { get; }
        public double? MaxStarRate { get; }
    }
}
