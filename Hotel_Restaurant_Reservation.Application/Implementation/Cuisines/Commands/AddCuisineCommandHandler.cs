using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands;

public class AddCuisineCommandHandler : ICommandHandler<AddCuisineCommand, Cuisine>
{
    private readonly IGenericRepository<Cuisine> _genericRepository;

    public AddCuisineCommandHandler(IGenericRepository<Cuisine> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Cuisine> Handle(AddCuisineCommand request, CancellationToken cancellationToken)
    {
        Cuisine cuisine = request.Cuisine;

        cuisine = await _genericRepository.AddAsync(cuisine);

        await _genericRepository.SaveChangesAsync();

        return cuisine;
    }
}
