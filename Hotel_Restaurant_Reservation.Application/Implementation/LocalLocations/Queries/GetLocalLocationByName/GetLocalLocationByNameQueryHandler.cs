using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationByName;

public class GetLocalLocationByNameQueryHandler : IQueryHandler<GetLocalLocationByNameQuery, Result<LocalLocationResponse>>
{
    private readonly IGenericRepository<LocalLocation> _localLocationRepository;
    private readonly IMapper _mapper;

    public GetLocalLocationByNameQueryHandler(IGenericRepository<LocalLocation> localLocationRepository, IMapper mapper)
    {
        _localLocationRepository = localLocationRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocalLocationResponse>> Handle(GetLocalLocationByNameQuery request, CancellationToken cancellationToken)
    {
        var localLocation = await _localLocationRepository.GetFirstOrDefaultAsync(l => l.Name == request.Name);

        if (localLocation is null)
        {
            return Result.Failure<LocalLocationResponse>(DomainErrors.LocalLocation.NotFoundByName(request.Name));
        }

        var localLocationResponse = _mapper.Map<LocalLocationResponse>(localLocation);
        return Result.Success(localLocationResponse);
    }
}