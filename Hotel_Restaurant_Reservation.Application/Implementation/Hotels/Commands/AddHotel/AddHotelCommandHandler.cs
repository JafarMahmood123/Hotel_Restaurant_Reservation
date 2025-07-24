using AutoMapper;
using FluentValidation;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Commands.AddHotel;
using Hotel_Restaurant_Reservation.Application.Implementation.Hotels.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;
using Hotel_Restaurant_Reservation.Domain.Shared;

public class AddHotelCommandHandler : ICommandHandler<AddHotelCommand, Result<HotelResponse>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IGenericRepository<Location> _locationRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddHotelRequest> _validator;

    public AddHotelCommandHandler(
        IHotelRepository hotelRepository,
        IGenericRepository<Location> locationRepository,
        IMapper mapper,
        IValidator<AddHotelRequest> validator)
    {
        _hotelRepository = hotelRepository;
        _locationRepository = locationRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<HotelResponse>> Handle(
        AddHotelCommand request,
        CancellationToken cancellationToken)
    {
        // Validate request
        if (request.AddHotelRequest == null)
            return Result.Failure<HotelResponse>(DomainErrors.Hotel.InvalidRequest);

        var validationResult = await _validator.ValidateAsync(request.AddHotelRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure<HotelResponse>(new Error(
                "Validation.Error",
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))));
        }

        // Handle location
        var locationResult = await HandleLocation(request.AddHotelRequest.LocationId);
        if (locationResult.IsFailure)
            return Result.Failure<HotelResponse>(locationResult.Error);

        // Create hotel with all required properties
        var hotel = _mapper.Map<Hotel>(request.AddHotelRequest);
        hotel.Id = Guid.NewGuid();
        hotel.Location = locationResult.Value;
        hotel.MinPrice = 0;
        hotel.MaxPrice = 0;

        // Save hotel
        hotel = await _hotelRepository.AddAsync(hotel);
        await _hotelRepository.SaveChangesAsync();

        // Map to response
        var response = _mapper.Map<HotelResponse>(hotel);

        return Result.Success(response);
    }

    private async Task<Result<Location>> HandleLocation(Guid locationId)
    {
        var existingLocation = await _locationRepository.GetFirstOrDefaultAsync(
            x => x.Id == locationId);

        if (existingLocation != null)
            return Result.Success(existingLocation);

        return Result.Failure<Location>(DomainErrors.Location.NotFound(locationId));
    }
}