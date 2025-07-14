using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Commands.AddLocalLocations;

public class AddLocalLocationCommandHandler : ICommandHandler<AddLocalLocationCommand, Result<LocalLocationResponse>>
{
    private readonly IGenericRepository<LocalLocation> _localLocationRepository;
    private readonly IGenericRepository<City> _cityRepository;
    private readonly IGenericRepository<CityLocalLocations> _cityLocalLocationRepository;
    private readonly IMapper _mapper;

    public AddLocalLocationCommandHandler(
        IGenericRepository<LocalLocation> localLocationRepository,
        IGenericRepository<City> cityRepository,
        IGenericRepository<CityLocalLocations> cityLocalLocationRepository,
        IMapper mapper)
    {
        _localLocationRepository = localLocationRepository;
        _cityRepository = cityRepository;
        _cityLocalLocationRepository = cityLocalLocationRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocalLocationResponse>> Handle(AddLocalLocationCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.AddLocalLocationRequest.CityId);
        if (city is null)
        {
            return Result.Failure<LocalLocationResponse>(DomainErrors.City.NotFound(request.AddLocalLocationRequest.CityId));
        }

        var localLocation = _mapper.Map<LocalLocation>(request.AddLocalLocationRequest);
        var existingLocation = await _localLocationRepository.GetFirstOrDefaultAsync(x => x.Name == localLocation.Name);
        if (existingLocation != null)
        {
            return Result.Failure<LocalLocationResponse>(DomainErrors.LocalLocation.ExistingLocalLocation(localLocation.Name));
        }

        localLocation.Id = Guid.NewGuid();
        await _localLocationRepository.AddAsync(localLocation);
        await _localLocationRepository.SaveChangesAsync();

        var cityLocalLocation = new CityLocalLocations
        {
            Id = Guid.NewGuid(),
            CityId = request.AddLocalLocationRequest.CityId,
            LocalLocationId = localLocation.Id
        };
        await _cityLocalLocationRepository.AddAsync(cityLocalLocation);
        await _cityLocalLocationRepository.SaveChangesAsync();


        var localLocationResponse = _mapper.Map<LocalLocationResponse>(localLocation);
        return Result.Success(localLocationResponse);
    }
}