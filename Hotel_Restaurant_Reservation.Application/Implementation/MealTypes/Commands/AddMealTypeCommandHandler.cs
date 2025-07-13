using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Queries;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands;

public class AddMealTypeCommandHandler : ICommandHandler<AddMealTypeCommand, Result<MealTypeResponse>>
{
    private readonly IGenericRepository<MealType> _mealTypeRepository;
    private readonly IMapper _mapper;

    public AddMealTypeCommandHandler(IGenericRepository<MealType> mealTypeRepository, IMapper mapper)
    {
        _mealTypeRepository = mealTypeRepository;
        this._mapper = mapper;
    }

    public async Task<Result<MealTypeResponse>> Handle(AddMealTypeCommand request, CancellationToken cancellationToken)
    {
        var mealType = _mapper.Map<MealType>(request.AddMealTypeRequest);

        var existingMealType = await _mealTypeRepository.GetFirstOrDefaultAsync(x => x.Name == mealType.Name);

        if (existingMealType != null)
            return Result.Failure<MealTypeResponse>(DomainErrors.MealType.ExistingMealType(mealType.Name));
        mealType.Id = Guid.NewGuid();

        mealType = await _mealTypeRepository.AddAsync(mealType);

        await _mealTypeRepository.SaveChangesAsync();

        var mealTypeResponse = _mapper.Map<MealTypeResponse>(mealType);

        return Result.Success(mealTypeResponse);
    }
}
