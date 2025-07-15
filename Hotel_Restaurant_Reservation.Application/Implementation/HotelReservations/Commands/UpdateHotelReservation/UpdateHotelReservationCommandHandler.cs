using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.UpdateHotelReservation
{
    public class UpdateHotelReservationCommandHandler : ICommandHandler<UpdateHotelReservationCommand, Result<HotelReservationResponse>>
    {
        private readonly IGenericRepository<HotelReservation> _hotelReservationRepository;
        private readonly IMapper _mapper;

        public UpdateHotelReservationCommandHandler(IGenericRepository<HotelReservation> hotelReservationRepository, IMapper mapper)
        {
            _hotelReservationRepository = hotelReservationRepository;
            _mapper = mapper;
        }

        public async Task<Result<HotelReservationResponse>> Handle(UpdateHotelReservationCommand request, CancellationToken cancellationToken)
        {
            var hotelReservation = await _hotelReservationRepository.GetByIdAsync(request.Id);

            if (hotelReservation is null)
            {
                return Result.Failure<HotelReservationResponse>(DomainErrors.HotelReservation.NotFound(request.Id));
            }

            if (hotelReservation.ReceivationEndDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return Result.Failure<HotelReservationResponse>(DomainErrors.HotelReservation.UpdateNotAllowedPastReservation());
            }

            _mapper.Map(request.UpdateHotelReservationRequest, hotelReservation);

            await _hotelReservationRepository.UpdateAsync(request.Id, hotelReservation);
            await _hotelReservationRepository.SaveChangesAsync();

            var hotelReservationResponse = _mapper.Map<HotelReservationResponse>(hotelReservation);

            return Result.Success(hotelReservationResponse);
        }
    }
}
