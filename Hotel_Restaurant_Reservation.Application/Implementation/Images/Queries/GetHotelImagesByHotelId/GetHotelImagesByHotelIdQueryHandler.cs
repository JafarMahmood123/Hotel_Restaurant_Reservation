using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Images.Queries.GetHotelImagesByHotelId
{
    public class GetHotelImagesByHotelIdQueryHandler : IQueryHandler<GetHotelImagesByHotelIdQuery, Result<List<string>>>
    {
        private readonly IGenericRepository<HotelImage> _hotelImageRepository;
        private readonly IGenericRepository<Hotel> _hotelRepository;

        public GetHotelImagesByHotelIdQueryHandler(IGenericRepository<HotelImage> hotelImageRepository, IGenericRepository<Hotel> hotelRepository)
        {
            _hotelImageRepository = hotelImageRepository;
            _hotelRepository = hotelRepository;
        }

        public async Task<Result<List<string>>> Handle(GetHotelImagesByHotelIdQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
            if (hotel is null)
            {
                return Result.Failure<List<string>>(DomainErrors.Hotel.NotFound(request.HotelId));
            }

            var images = await _hotelImageRepository.Where(ei => ei.HotelId == request.HotelId).ToListAsync(cancellationToken);

            if (!images.Any())
            {
                return Result.Failure<List<string>>(DomainErrors.Hotel.NoImagesFound);
            }

            return Result.Success(images.Select(i => i.Url).ToList());
        }
    }
}