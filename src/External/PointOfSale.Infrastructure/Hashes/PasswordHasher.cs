using System.Security.Cryptography;
using System.Text;
using PointOfSale.Model.Services;

namespace PointOfSale.Infrastructure.Hashes;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 32;
    private const int KeySize = 32;
    private const int Iterations = 600000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;
    public string Hash(string password)
    {
      //  using var rng = RandomNumberGenerator.Create();
      //  byte[] salt = new byte[SaltSize];
       // rng.GetBytes(salt);
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        
        var hash  = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),
            salt, Iterations, 
            HashAlgorithm,  KeySize);

        return $"{Iterations}.{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";
        // byte[] hashBytes =  new byte[48];
        // Array.Copy(salt, 0, hashBytes, 0, 16);
        // Array.Copy(pbkdf2, 0, hashBytes, 16, 20);
        // return Convert.ToBase64String(hashBytes);
    }

    public bool Verify(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split('.');
        int iterations = int.Parse(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);
        byte[] storeHash = Convert.FromHexString(parts[2]);

        byte[] currentHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithm,
            storeHash.Length);
        return CryptographicOperations.FixedTimeEquals(storeHash, currentHash);
    }
}