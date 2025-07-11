using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AssignPropertyTypeToHotel;

public class AssignPropertyTypeToHotelCommandHandler : ICommandHandler<AssignPropertyTypeToHotelCommand, Result<PropertyTypeResponse>>
{
    private readonly IGenericRepository<Hotel> _hotelRepository;
    private readonly IGenericRepository<PropertyType> _propertyTypeRepository;
    private readonly IMapper _mapper;

    public AssignPropertyTypeToHotelCommandHandler(IGenericRepository<Hotel> hotelRepository,
        IGenericRepository<PropertyType> propertyTypeRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _propertyTypeRepository = propertyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyTypeResponse>> Handle(AssignPropertyTypeToHotelCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
        if (hotel is null)
        {
            return Result.Failure<PropertyTypeResponse>(DomainErrors.Hotel.NotFound(request.HotelId));
        }

        var propertyType = await _propertyTypeRepository.GetByIdAsync(request.PropertyTypeId);
        if (propertyType is null)
        {
            return Result.Failure<PropertyTypeResponse>(DomainErrors.PropertyType.NotFound(request.PropertyTypeId));
        }

        hotel.PropertyTypeId = request.PropertyTypeId;

        await _hotelRepository.UpdateAsync(request.HotelId, hotel);
        await _hotelRepository.SaveChangesAsync();


        var response = _mapper.Map<PropertyTypeResponse>(propertyType);

        return Result.Success(response);
    }
}