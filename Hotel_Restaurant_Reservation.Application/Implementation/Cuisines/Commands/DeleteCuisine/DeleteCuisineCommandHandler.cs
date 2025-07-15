using System.Threading;
using System.Threading.Tasks;
using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.Cuisines.Commands.DeleteCuisine
{
    public class DeleteCuisineCommandHandler : ICommandHandler<DeleteCuisineCommand, Result>
    {
        private readonly IGenericRepository<Cuisine> _cuisineRepository;

        public DeleteCuisineCommandHandler(IGenericRepository<Cuisine> cuisineRepository)
        {
            _cuisineRepository = cuisineRepository;
        }

        public async Task<Result> Handle(DeleteCuisineCommand request, CancellationToken cancellationToken)
        {
            var cuisine = await _cuisineRepository.GetByIdAsync(request.Id);

            if (cuisine is null)
            {
                return Result.Failure(DomainErrors.Cuisine.NotFound(request.Id));
            }

            await _cuisineRepository.RemoveAsync(request.Id);
            await _cuisineRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}