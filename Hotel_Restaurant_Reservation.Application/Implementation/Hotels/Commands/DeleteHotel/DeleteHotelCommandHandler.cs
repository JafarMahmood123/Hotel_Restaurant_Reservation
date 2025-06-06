﻿using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.Hotel;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommandHandler : ICommandHandler<DeleteHotelCommand, HotelResponse?>
{
    private readonly IGenericRepository<Hotel> hotelRepository;
    private readonly IMapper mapper;

    public DeleteHotelCommandHandler(IGenericRepository<Hotel> hotelRepository, IMapper mapper)
    {
        this.hotelRepository = hotelRepository;
        this.mapper = mapper;
    }

    public async Task<HotelResponse?> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        var hotelId = request.HotelId;

        var hotel = await hotelRepository.RemoveAsync(hotelId);

        if (hotel == null) 
            return null;

        return mapper.Map<HotelResponse>(hotel);
    }
}
