using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.JwtProvider;

public interface IJwtProvider
{
    public string Generate(Customer customer, Role role);
}
