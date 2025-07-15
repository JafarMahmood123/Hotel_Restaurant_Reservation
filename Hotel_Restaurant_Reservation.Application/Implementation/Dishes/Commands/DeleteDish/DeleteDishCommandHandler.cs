using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandHandler : ICommandHandler<DeleteDishCommand, Result>
    {
        private readonly IGenericRepository<Dish> _dishRepository;

        public DeleteDishCommandHandler(IGenericRepository<Dish> dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<Result> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetByIdAsync(request.Id);

            if (dish is null)
            {
                return Result.Failure(DomainErrors.Dish.NotFound(request.Id));
            }

            await _dishRepository.RemoveAsync(request.Id);
            await _dishRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}