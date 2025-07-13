using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries.GetHotelReservationById;

public class GetHotelReservationByIdQueryHandler : IQueryHandler<GetHotelReservationByIdQuery, Result<HotelReservationResponse>>
{
    private readonly IGenericRepository<HotelReservation> _hotelReservationRepository;
    private readonly IMapper _mapper;

    public GetHotelReservationByIdQueryHandler(IGenericRepository<HotelReservation> hotelReservationRepository, IMapper mapper)
    {
        _hotelReservationRepository = hotelReservationRepository;
        _mapper = mapper;
    }

    public async Task<Result<HotelReservationResponse>> Handle(GetHotelReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var hotelReservation = await _hotelReservationRepository.GetByIdAsync(request.Id);

        if (hotelReservation is null)
        {
            return Result.Failure<HotelReservationResponse>(DomainErrors.HotelReservation.NotFound(request.Id));
        }

        var hotelReservationResponse = _mapper.Map<HotelReservationResponse>(hotelReservation);
        return Result.Success(hotelReservationResponse);
    }
}