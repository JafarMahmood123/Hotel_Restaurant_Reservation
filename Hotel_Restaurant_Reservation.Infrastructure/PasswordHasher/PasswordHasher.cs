using Hotel_Restaurant_Reservation.Application.Abstractions.PasswordHasher;
using System.Security.Cryptography;

namespace Hotel_Restaurant_Reservation.Infrastructure.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    private const int _SALT_SIZE = 16;
    private const int _HASH_SIZE = 32;
    private const int _ITERATIONS = 10000;

    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_SALT_SIZE);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _ITERATIONS, _algorithm, _HASH_SIZE);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split("-");

        var hash = Convert.FromHexString(parts[0]);
        var salt = Convert.FromHexString(parts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _ITERATIONS, _algorithm, _HASH_SIZE);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
