using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Repositories
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<IEnumerable<Hotel>?> GetFilteredHotelsAsync(
            Guid? countryId,
            Guid? cityId,
            Guid? localLocationId,
            Guid? propertyTypeId,
            Guid? amenityId,
            double? minPrice,
            double? maxPrice,
            double? minStarRate,
            double? maxStarRate);
    }
}
