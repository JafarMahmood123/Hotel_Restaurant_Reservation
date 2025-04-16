using MediatR;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
