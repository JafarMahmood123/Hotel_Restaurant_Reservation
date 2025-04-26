using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.MealTypes.Commands;

public class AddMealTypeCommandHandler : ICommandHandler<AddMealTypeCommand, MealType>
{
    private readonly IGenericRepository<MealType> _genericRepository;

    public AddMealTypeCommandHandler(IGenericRepository<MealType> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<MealType> Handle(AddMealTypeCommand request, CancellationToken cancellationToken)
    {
        MealType mealType = request.MealType;

        mealType = await _genericRepository.AddAsync(mealType);

        await _genericRepository.SaveChangesAsync();

        return mealType;
    }
}
