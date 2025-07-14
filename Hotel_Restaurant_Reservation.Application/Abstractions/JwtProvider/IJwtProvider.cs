using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.JwtProvider;

public interface IJwtProvider
{
    public string Generate(User customer, Role role);
}
