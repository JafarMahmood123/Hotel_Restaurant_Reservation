using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Infrastructure; // Namespace for your DbContext
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Infrastructure.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly HotelRestaurantDbContext _hotelRestaurantDbContext;

        public HotelRepository(HotelRestaurantDbContext hotelRestaurantDbContext) : base(hotelRestaurantDbContext)
        {
            _hotelRestaurantDbContext = hotelRestaurantDbContext;
        }

        /// <summary>
        /// Builds a filterable query for Hotels.
        /// NOTE: This method is now only responsible for applying WHERE clauses.
        /// The caller is responsible for adding any necessary .Include() statements
        /// before executing the query.
        /// </summary>
        public IQueryable<Hotel> GetFilteredHotelsQuery(
            Guid? countryId = null, Guid? cityId = null, Guid? localLocationId = null, Guid? propertyTypeId = null,
            Guid? amenityId = null, double? minPrice = null, double? maxPrice = null, double? minStarRate = null, double? maxStarRate = null)
        {
            // Start with the base DbSet. No .Include() statements are used here.
            IQueryable<Hotel> hotelsQuery = _hotelRestaurantDbContext.Hotels;

            // Conditionally apply each filter.
            // These filters will still work because they rely on navigation properties
            // that EF Core translates into the necessary SQL JOINs.
            if (countryId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.Location.CountryId == countryId);

            if (cityId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.Location.CityLocalLocations.CityId == cityId);

            if (localLocationId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.Location.CityLocalLocations.LocalLocationId == localLocationId);

            if (propertyTypeId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.PropertyTypeId == propertyTypeId);

            if (amenityId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.HotelAmenitiesPrices.Any(a => a.AmenityId == amenityId));

            if (minPrice.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.MinPrice >= minPrice);

            if (maxPrice.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.MaxPrice <= maxPrice);

            if (minStarRate.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.StarRate >= minStarRate);

            if (maxStarRate.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.StarRate <= maxStarRate);

            // Return the composed query without executing it.
            return hotelsQuery;
        }
    }
}
