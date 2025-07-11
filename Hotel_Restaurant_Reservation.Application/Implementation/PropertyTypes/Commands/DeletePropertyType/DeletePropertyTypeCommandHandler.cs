using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.PropertyTypes.Commands.DeletePropertyType;

public class DeletePropertyTypeCommandHandler : ICommandHandler<DeletePropertyTypeCommand, Result>
{
    private readonly IGenericRepository<PropertyType> _propertyTypeRepository;

    public DeletePropertyTypeCommandHandler(IGenericRepository<PropertyType> propertyTypeRepository)
    {
        _propertyTypeRepository = propertyTypeRepository;
    }

    public async Task<Result> Handle(DeletePropertyTypeCommand request, CancellationToken cancellationToken)
    {
        var propertyType = await _propertyTypeRepository.GetByIdAsync(request.Id);

        if (propertyType is null)
        {
            return Result.Failure(DomainErrors.PropertyType.NotFound(request.Id));
        }

        await _propertyTypeRepository.RemoveAsync(request.Id);
        await _propertyTypeRepository.SaveChangesAsync();

        return Result.Success();
    }
}