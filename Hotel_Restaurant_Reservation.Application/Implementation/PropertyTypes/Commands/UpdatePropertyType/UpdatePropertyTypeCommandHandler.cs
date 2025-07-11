using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.UpdatePropertyType;

public class UpdatePropertyTypeCommandHandler : ICommandHandler<UpdatePropertyTypeCommand, Result<PropertyType>>
{
    private readonly IGenericRepository<PropertyType> _propertyTypeRepository;
    private readonly IMapper _mapper;

    public UpdatePropertyTypeCommandHandler(IGenericRepository<PropertyType> propertyTypeRepository, IMapper mapper)
    {
        _propertyTypeRepository = propertyTypeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PropertyType>> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
    {
        var propertyType = await _propertyTypeRepository.GetByIdAsync(request.Id);

        if (propertyType is null)
        {
            return Result.Failure<PropertyType>(DomainErrors.PropertyType.NotFound(request.Id));
        }

        _mapper.Map(request.UpdatePropertyTypeRequest, propertyType);

        await _propertyTypeRepository.UpdateAsync(request.Id, propertyType);
        await _propertyTypeRepository.SaveChangesAsync();

        return Result.Success(propertyType);
    }
}