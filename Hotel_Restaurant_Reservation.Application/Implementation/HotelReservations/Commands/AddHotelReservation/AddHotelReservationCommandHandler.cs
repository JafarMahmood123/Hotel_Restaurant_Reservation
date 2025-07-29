using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Hotel_Restaurant_Reservation.Application.Abstractions.Payment;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.AddHotelReservation;

public class AddHotelReservationCommandHandler : ICommandHandler<AddHotelReservationCommand, Result<HotelReservationResponse>>
{
    private readonly IGenericRepository<HotelReservation> _hotelReservationRepository;
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IGenericRepository<HotelReservationPayment> _hotelReservationPaymentRepository;
    private readonly IMapper _mapper;

    public AddHotelReservationCommandHandler(
        IGenericRepository<HotelReservation> hotelReservationRepository,
        IGenericRepository<Room> roomRepository,
        IGenericRepository<HotelReservationPayment> hotelReservationPaymentRepository,
        IMapper mapper)
    {
        _hotelReservationRepository = hotelReservationRepository;
        _roomRepository = roomRepository;
        _hotelReservationPaymentRepository = hotelReservationPaymentRepository;
        _mapper = mapper;
    }

    public async Task<Result<HotelReservationResponse>> Handle(AddHotelReservationCommand request, CancellationToken cancellationToken)
    {
        var hotelReservation = _mapper.Map<HotelReservation>(request.AddHotelReservationRequest);
        hotelReservation.Id = Guid.NewGuid();
        hotelReservation.ReservationDateTime = DateTime.UtcNow;
        hotelReservation.UserId = request.AddHotelReservationRequest.CustomerId;

        var room = await _roomRepository.GetByIdAsync(request.AddHotelReservationRequest.RoomId);
        if (room == null)
        {
            return Result.Failure<HotelReservationResponse>(DomainErrors.Room.NotFound(request.AddHotelReservationRequest.RoomId));
        }

        if(request.AddHotelReservationRequest.NumberOfPeople > room.MaxOccupancy)
        {
            return Result.Failure<HotelReservationResponse>(DomainErrors.HotelReservation.InvalidNumberOfPeople());
        }

        var numberOfNights = hotelReservation.ReceivationEndDate.DayNumber - hotelReservation.ReceivationStartDate.DayNumber;
        var totalAmount = (decimal)room.Price * numberOfNights;

        var createdHotelReservation = await _hotelReservationRepository.AddAsync(hotelReservation);
        await _hotelReservationRepository.SaveChangesAsync();

        var hotelReservationPayment = new HotelReservationPayment
        {
            Id = Guid.NewGuid(),
            HotelReservationId = createdHotelReservation.Id,
            Amount = totalAmount,
            Status = Domain.Enums.PaymentStatus.Pending,
        };
        await _hotelReservationPaymentRepository.AddAsync(hotelReservationPayment);
        await _hotelReservationPaymentRepository.SaveChangesAsync();

        var hotelReservationResponse = _mapper.Map<HotelReservationResponse>(createdHotelReservation);

        return Result.Success(hotelReservationResponse);
    }
}