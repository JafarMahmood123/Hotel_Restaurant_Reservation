using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries.GetPropertyTypeById;

public class GetPropertyTypeByIdQueryHandler : IQueryHandler<GetPropertyTypeByIdQuery, Result<PropertyTypeResponse>>
{
    private readonly IGenericRepository<PropertyType> _propertyTypeRepository;
    private readonly IMapper _mapper;

    public GetPropertyTypeByIdQueryHandler(IGenericRepository<PropertyType> propertyTypeRepository, IMapper mapper)
    {
        _propertyTypeRepository = propertyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyTypeResponse>> Handle(GetPropertyTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var propertyType = await _propertyTypeRepository.GetByIdAsync(request.Id);

        if (propertyType is null)
        {
            return Result.Failure<PropertyTypeResponse>(DomainErrors.PropertyType.NotFound(request.Id));
        }

        var propertyTypeResponse = _mapper.Map<PropertyTypeResponse>(propertyType);
        return Result.Success(propertyTypeResponse);
    }
}