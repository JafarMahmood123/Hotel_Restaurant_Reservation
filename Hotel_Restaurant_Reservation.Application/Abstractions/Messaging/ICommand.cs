using MediatR;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
