using Hotel_Restaurant_Reservation.Domain.Entities;

namespace Hotel_Restaurant_Reservation.Application.Abstractions;

public interface IJwtProvider
{
    public string Generate(Customer customer, IEnumerable<Role> roles);
}
