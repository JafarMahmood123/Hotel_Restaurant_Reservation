using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationsByCityId;

public class GetLocalLocationsByCityIdQueryHandler : IQueryHandler<GetLocalLocationsByCityIdQuery, Result<IEnumerable<LocalLocationResponse>>>
{
    private readonly IGenericRepository<CityLocalLocations> _cityLocalLocationRepository;
    private readonly IMapper _mapper;

    public GetLocalLocationsByCityIdQueryHandler(IGenericRepository<CityLocalLocations> cityLocalLocationRepository, IMapper mapper)
    {
        _cityLocalLocationRepository = cityLocalLocationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<LocalLocationResponse>>> Handle(GetLocalLocationsByCityIdQuery request, CancellationToken cancellationToken)
    {
        var cityLocalLocations = await _cityLocalLocationRepository
            .Where(cll => cll.CityId == request.CityId)
            .Include(cll => cll.LocalLocation)
            .ToListAsync(cancellationToken);

        var localLocations = cityLocalLocations.Select(cll => cll.LocalLocation);

        var localLocationResponses = _mapper.Map<IEnumerable<LocalLocationResponse>>(localLocations);

        return Result.Success(localLocationResponses);
    }
}