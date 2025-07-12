using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReservations.Commands.DeleteHotelReservation;

public class DeleteHotelReservationCommandHandler : ICommandHandler<DeleteHotelReservationCommand, Result>
{
    private readonly IGenericRepository<HotelReservation> _hotelReservationRepository;

    public DeleteHotelReservationCommandHandler(IGenericRepository<HotelReservation> hotelReservationRepository)
    {
        _hotelReservationRepository = hotelReservationRepository;
    }

    public async Task<Result> Handle(DeleteHotelReservationCommand request, CancellationToken cancellationToken)
    {
        var hotelReservation = await _hotelReservationRepository.GetByIdAsync(request.Id);

        if (hotelReservation is null)
        {
            return Result.Failure(DomainErrors.HotelReservation.NotFound(request.Id));
        }

        await _hotelReservationRepository.RemoveAsync(request.Id);
        await _hotelReservationRepository.SaveChangesAsync();

        return Result.Success();
    }
}