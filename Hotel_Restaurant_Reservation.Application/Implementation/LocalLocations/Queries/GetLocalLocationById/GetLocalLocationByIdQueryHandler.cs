using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.LocalLocations.Queries.GetLocalLocationById;

public class GetLocalLocationByIdQueryHandler : IQueryHandler<GetLocalLocationByIdQuery, Result<LocalLocationResponse>>
{
    private readonly IGenericRepository<LocalLocation> _localLocationRepository;
    private readonly IMapper _mapper;

    public GetLocalLocationByIdQueryHandler(IGenericRepository<LocalLocation> localLocationRepository, IMapper mapper)
    {
        _localLocationRepository = localLocationRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocalLocationResponse>> Handle(GetLocalLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var localLocation = await _localLocationRepository.GetByIdAsync(request.Id);

        if (localLocation is null)
        {
            return Result.Failure<LocalLocationResponse>(DomainErrors.LocalLocation.NotFound(request.Id));
        }

        var localLocationResponse = _mapper.Map<LocalLocationResponse>(localLocation);
        return Result.Success(localLocationResponse);
    }
}