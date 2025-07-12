using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Hotel>?> GetFilteredHotelsAsync(
            Guid? countryId,
            Guid? cityId,
            Guid? localLocationId,
            Guid? propertyTypeId,
            Guid? amenityId,
            double? minPrice,
            double? maxPrice,
            double? minStarRate,
            double? maxStarRate)
        {
            IQueryable<Hotel> hotelsQuery = _hotelRestaurantDbContext.Hotels
                .Include(h => h.Location)
                    .ThenInclude(l => l.CityLocalLocations)
                        .ThenInclude(cll => cll.City)
                .Include(h => h.Location)
                    .ThenInclude(l => l.CityLocalLocations)
                        .ThenInclude(cll => cll.LocalLocation)
                .Include(h => h.Location)
                    .ThenInclude(l => l.Country)
                .Include(h => h.PropertyType)
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.RoomAmenities)
                        .ThenInclude(ra => ra.Amenity)
                .Include(h => h.HotelRangePrices);

            if (countryId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.Location.CountryId == countryId);

            if (cityId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.Location.CityLocalLocations.CityId == cityId);

            if (localLocationId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.Location.CityLocalLocations.LocalLocationId == localLocationId);

            if (propertyTypeId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.PropertyTypeId == propertyTypeId);

            if (amenityId.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.Rooms.Any(r => r.RoomAmenities.Any(ra => ra.AmenityId == amenityId)));

            if (minPrice.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.HotelRangePrices.MinPrice >= minPrice);

            if (maxPrice.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.HotelRangePrices.MaxPrice <= maxPrice);

            if (minStarRate.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.StarRate >= minStarRate);

            if (maxStarRate.HasValue)
                hotelsQuery = hotelsQuery.Where(h => h.StarRate <= maxStarRate);

            return await hotelsQuery.ToListAsync();
        }
    }
}
