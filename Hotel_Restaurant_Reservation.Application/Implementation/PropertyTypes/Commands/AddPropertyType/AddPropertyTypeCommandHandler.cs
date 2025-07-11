using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.AddPropertyType;

public class AddPropertyTypeCommandHandler : ICommandHandler<AddPropertyTypeCommand, Result<PropertyTypeResponse>>
{
    private readonly IGenericRepository<PropertyType> _propertyTypeRepository;
    private readonly IMapper _mapper;

    public AddPropertyTypeCommandHandler(IGenericRepository<PropertyType> propertyTypeRepository, IMapper mapper)
    {
        _propertyTypeRepository = propertyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyTypeResponse>> Handle(AddPropertyTypeCommand request, CancellationToken cancellationToken)
    {
        var propertyType = _mapper.Map<PropertyType>(request.AddPropertyTypeRequest);

        var existingPropertyType = await _propertyTypeRepository.GetFirstOrDefaultAsync(p => p.Name == request.AddPropertyTypeRequest.Name);
        if (existingPropertyType != null)
        {
            return Result.Failure<PropertyTypeResponse>(DomainErrors.PropertyType.NotFound(request.AddPropertyTypeRequest.Name));
        }

        propertyType.Id = Guid.NewGuid();
        await _propertyTypeRepository.AddAsync(propertyType);
        await _propertyTypeRepository.SaveChangesAsync();

        var response = _mapper.Map<PropertyTypeResponse>(propertyType);
        return Result.Success(response);
    }
}