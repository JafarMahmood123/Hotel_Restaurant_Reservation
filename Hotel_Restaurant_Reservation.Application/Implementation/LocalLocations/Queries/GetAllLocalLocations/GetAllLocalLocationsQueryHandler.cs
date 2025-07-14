using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetAllLocalLocations;

public class GetAllLocalLocationsQueryHandler : IQueryHandler<GetAllLocalLocationsQuery, Result<IEnumerable<LocalLocationResponse>>>
{
    private readonly IGenericRepository<LocalLocation> _localLocationRepository;
    private readonly IMapper _mapper;

    public GetAllLocalLocationsQueryHandler(IGenericRepository<LocalLocation> localLocationRepository, IMapper mapper)
    {
        _localLocationRepository = localLocationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<LocalLocationResponse>>> Handle(GetAllLocalLocationsQuery request, CancellationToken cancellationToken)
    {
        var localLocations = await _localLocationRepository.GetAllAsync();
        var localLocationResponses = _mapper.Map<IEnumerable<LocalLocationResponse>>(localLocations);
        return Result.Success(localLocationResponses);
    }
}