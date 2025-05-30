using AutoMapper;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.DTOs.DishDTOs;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Errors;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.AddDish;

public class AddDishCommandHandler : ICommandHandler<AddDishCommand, Result<DishResponse>>
{
    private readonly IGenericRepository<Dish> _dishRepository;
    private readonly IMapper _mapper;

    public AddDishCommandHandler(IGenericRepository<Dish> dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        this._mapper = mapper;
    }

    public async Task<Result<DishResponse>> Handle(AddDishCommand request, CancellationToken cancellationToken)
    {
        Dish dish = _mapper.Map<Dish>(request.AddDishRequest);

        var existingDish = await _dishRepository.GetFirstOrDefaultAsync(x => x.Name == dish.Name);

        if (existingDish != null)
            return Result.Failure<DishResponse>(DomainErrors.Dish.ExistingDish);

        dish.Id = Guid.NewGuid();

        dish = await _dishRepository.AddAsync(dish);

        await _dishRepository.SaveChangesAsync();

        var dishResponse = _mapper.Map<DishResponse>(dish);

        return Result.Success(dishResponse);
    }
}
