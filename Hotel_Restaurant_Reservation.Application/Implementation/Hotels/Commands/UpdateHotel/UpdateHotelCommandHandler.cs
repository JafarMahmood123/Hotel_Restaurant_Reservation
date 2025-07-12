using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.UpdateHotel;

public class UpdateHotelCommandHandler : ICommandHandler<UpdateHotelCommand, Result<HotelResponse>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<Result<HotelResponse>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id);

        if (hotel is null)
        {
            return Result.Failure<HotelResponse>(DomainErrors.Hotel.NotFound(request.Id));
        }

        _mapper.Map(request.UpdateHotelRequest, hotel);

        await _hotelRepository.UpdateAsync(request.Id, hotel);
        await _hotelRepository.SaveChangesAsync();

        var hotelResponse = _mapper.Map<HotelResponse>(hotel);

        return Result.Success(hotelResponse);
    }
}