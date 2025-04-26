using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.AddDish;

public class AddDishCommandHandler : ICommandHandler<AddDishCommand, Dish>
{
    private readonly IGenericRepository<Dish> _genericRepository;

    public AddDishCommandHandler(IGenericRepository<Dish> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Dish> Handle(AddDishCommand request, CancellationToken cancellationToken)
    {
        Dish dish = request.Dish;

        dish = await _genericRepository.AddAsync(dish);

        await _genericRepository.SaveChangesAsync();

        return dish;
    }
}
