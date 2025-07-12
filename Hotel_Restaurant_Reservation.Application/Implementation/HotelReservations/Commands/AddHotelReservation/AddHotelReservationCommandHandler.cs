using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.AddHotelReservation;

public class AddHotelReservationCommandHandler : ICommandHandler<AddHotelReservationCommand, Result<HotelReservationResponse>>
{
    private readonly IGenericRepository<HotelReservation> _hotelReservationRepository;
    private readonly IMapper _mapper;

    public AddHotelReservationCommandHandler(IGenericRepository<HotelReservation> hotelReservationRepository, IMapper mapper)
    {
        _hotelReservationRepository = hotelReservationRepository;
        _mapper = mapper;
    }

    public async Task<Result<HotelReservationResponse>> Handle(AddHotelReservationCommand request, CancellationToken cancellationToken)
    {
        var hotelReservation = _mapper.Map<HotelReservation>(request.AddHotelReservationRequest);
        hotelReservation.Id = Guid.NewGuid();
        hotelReservation.ReservationDateTime = DateTime.UtcNow;

        var createdHotelReservation = await _hotelReservationRepository.AddAsync(hotelReservation);
        await _hotelReservationRepository.SaveChangesAsync();

        var hotelReservationResponse = _mapper.Map<HotelReservationResponse>(createdHotelReservation);

        return Result.Success(hotelReservationResponse);
    }
}