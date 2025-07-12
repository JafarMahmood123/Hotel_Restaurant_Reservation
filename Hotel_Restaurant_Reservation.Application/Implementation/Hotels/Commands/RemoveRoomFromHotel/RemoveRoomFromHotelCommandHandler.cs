using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.RemoveRoomFromHotel;

public class RemoveRoomFromHotelCommandHandler : ICommandHandler<RemoveRoomFromHotelCommand, Result>
{
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IHotelRepository _hotelRepository;

    public RemoveRoomFromHotelCommandHandler(IGenericRepository<Room> roomRepository, IHotelRepository hotelRepository)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
    }

    public async Task<Result> Handle(RemoveRoomFromHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        if (hotel == null)
        {
            return Result.Failure(DomainErrors.Hotel.NotFound(request.HotelId));
        }

        var room = await _roomRepository.GetByIdAsync(request.RoomId);

        if (room is null || room.HotelId != request.HotelId)
        {
            return Result.Failure(DomainErrors.Room.NotFound(request.RoomId));
        }

        await _roomRepository.RemoveAsync(request.RoomId);

        // Decrement the number of rooms in the hotel
        hotel.NumberOfRooms--;

        await _roomRepository.SaveChangesAsync();

        return Result.Success();
    }
}