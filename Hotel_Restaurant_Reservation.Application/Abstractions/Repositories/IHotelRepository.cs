using Hotel_Restaurant_Reservation.Domain.Entities;
using System;
using System.Linq;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Repositories
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        IQueryable<Hotel> GetFilteredHotelsQuery(
            Guid? countryId = null,
            Guid? cityId = null,
            Guid? localLocationId = null,
            Guid? propertyTypeId = null,
            Guid? amenityId = null,
            double? minPrice = null,
            double? maxPrice = null,
            double? minStarRate = null,
            double? maxStarRate = null);
    }
}
