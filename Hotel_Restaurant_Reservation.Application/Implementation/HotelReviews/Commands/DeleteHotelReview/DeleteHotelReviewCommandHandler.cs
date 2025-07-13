using Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;
using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Shared;

namespace Hotel_Restaurant_Reservation.Application.Implementation.HotelReviews.Commands.DeleteHotelReview;

public class DeleteHotelReviewCommandHandler : ICommandHandler<DeleteHotelReviewCommand, Result>
{
    private readonly IGenericRepository<HotelReview> _hotelReviewRepository;

    public DeleteHotelReviewCommandHandler(IGenericRepository<HotelReview> hotelReviewRepository)
    {
        _hotelReviewRepository = hotelReviewRepository;
    }

    public async Task<Result> Handle(DeleteHotelReviewCommand request, CancellationToken cancellationToken)
    {
        var hotelReview = await _hotelReviewRepository.GetByIdAsync(request.Id);

        if (hotelReview is null)
        {
            return Result.Failure(DomainErrors.HotelReview.NotFound(request.Id));
        }

        await _hotelReviewRepository.RemoveAsync(request.Id);
        await _hotelReviewRepository.SaveChangesAsync();

        return Result.Success();
    }
}