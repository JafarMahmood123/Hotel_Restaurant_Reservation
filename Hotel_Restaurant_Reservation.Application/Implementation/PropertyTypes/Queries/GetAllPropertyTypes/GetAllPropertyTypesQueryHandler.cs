using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries.GetAllPropertyTypes;

public class GetAllPropertyTypesQueryHandler : IQueryHandler<GetAllPropertyTypesQuery, Result<IEnumerable<PropertyTypeResponse>>>
{
    private readonly IGenericRepository<PropertyType> _propertyTypeRepository;
    private readonly IMapper _mapper;

    public GetAllPropertyTypesQueryHandler(IGenericRepository<PropertyType> propertyTypeRepository, IMapper mapper)
    {
        _propertyTypeRepository = propertyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<PropertyTypeResponse>>> Handle(GetAllPropertyTypesQuery request, CancellationToken cancellationToken)
    {
        var propertyTypes = await _propertyTypeRepository.GetAllAsync();
        var propertyTypeResponses = _mapper.Map<IEnumerable<PropertyTypeResponse>>(propertyTypes);
        return Result.Success(propertyTypeResponses);
    }
}

