using Hotel_Restaurant_Reservation.Domain.Entities;
using Hotel_Restaurant_Reservation.Domain.Enums;

namespace Hotel_Restaurant_Reservation.Application.Abstractions;

public interface IJwtProvider
{
    public string Generate(Customer customer, Roles role);
}
