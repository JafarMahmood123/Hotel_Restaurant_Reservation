using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries; // For HotelResponse
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries.GetAllHotels
{
    public class GetAllHotelsQueryHandler : IQueryHandler<GetAllHotelsQuery, Result<PagedResult<HotelResponse>>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetAllHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<HotelResponse>>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Get the base IQueryable from the repository.
                var hotelsQuery = _hotelRepository.GetFilteredHotelsQuery(
                    request.CountryId,
                    request.CityId,
                    request.LocalLocationId,
                    request.PropertyTypeId,
                    request.AmenityId,
                    request.MinPrice,
                    request.MaxPrice,
                    request.MinStarRate,
                    request.MaxStarRate);

                // 2. Get the total count for pagination metadata.
                var totalCount = await hotelsQuery.CountAsync(cancellationToken);

                // 3. Apply pagination to the IQueryable.
                var pagedHotels = await hotelsQuery
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                // 4. Map the results to the response DTO
                var responseItems = _mapper.Map<List<HotelResponse>>(pagedHotels);

                // 5. Create the PagedResult object
                var pagedResult = new PagedResult<HotelResponse>(
                    responseItems,
                    request.Page,
                    request.PageSize,
                    totalCount);

                return Result.Success(pagedResult);
            }
            catch (Exception ex)
            {
                return Result.Failure<PagedResult<HotelResponse>>(
                    new Error("Hotel.QueryError", $"An error occurred while retrieving hotels: {ex.Message}"));
            }
        }
    }
}
