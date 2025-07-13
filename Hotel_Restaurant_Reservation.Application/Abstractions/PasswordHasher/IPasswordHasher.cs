namespace Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;

public interface IPasswordHasher
{
    public string Hash(string password);

    public string Verify(string password, string passwordHash);
}
