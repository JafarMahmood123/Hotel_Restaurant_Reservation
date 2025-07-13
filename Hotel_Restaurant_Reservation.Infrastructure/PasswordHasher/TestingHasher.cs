using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;

namespace Hotel_Restaurant_Reservation.Infrastructure.PasswordHasher;

public class TestingHasher : IPasswordHasher
{
    public string Hash(string password) => password;

    public bool Verify(string password, string passwordHash) => password.Equals(passwordHash);
}
