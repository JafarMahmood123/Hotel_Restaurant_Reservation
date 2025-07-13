using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetFilteredHotels
{
    public class GetFilteredHotelsQueryHandler : IQueryHandler<GetFilteredHotelsQuery, Result<IEnumerable<HotelResponse>>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetFilteredHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<HotelResponse>>> Handle(GetFilteredHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _hotelRepository.GetFilteredHotelsAsync(
                request.CountryId,
                request.CityId,
                request.LocalLocationId,
                request.PropertyTypeId,
                request.AmenityId,
                request.MinPrice,
                request.MaxPrice,
                request.MinStarRate,
                request.MaxStarRate);

            if (hotels is null || !hotels.Any())
            {
                return Result.Failure<IEnumerable<HotelResponse>>(DomainErrors.Hotel.NotFoundForFilters);
            }

            var hotelResponses = _mapper.Map<IEnumerable<HotelResponse>>(hotels);

            return Result.Success(hotelResponses);
        }
    }
}
