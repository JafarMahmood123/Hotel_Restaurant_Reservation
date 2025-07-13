using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoomToHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Rooms.Commands.AddRoom;

public class AddRoomToHotelCommandHandler : ICommandHandler<AddRoomToHotelCommand, Result<RoomResponse>>
{
    private readonly IGenericRepository<Room> _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public AddRoomToHotelCommandHandler(IGenericRepository<Room> roomRepository, IHotelRepository hotelRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<Result<RoomResponse>> Handle(AddRoomToHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        if (hotel == null)
        {
            return Result.Failure<RoomResponse>(DomainErrors.Hotel.NotFound(request.HotelId));
        }

        var room = _mapper.Map<Room>(request.AddRoomRequest);
        room.Id = Guid.NewGuid();
        room.HotelId = request.HotelId;

        var createdRoom = await _roomRepository.AddAsync(room);

        // Increment the number of rooms in the hotel
        hotel.NumberOfRooms++;

        await _roomRepository.SaveChangesAsync();

        var roomResponse = _mapper.Map<RoomResponse>(createdRoom);

        return Result.Success(roomResponse);
    }
}