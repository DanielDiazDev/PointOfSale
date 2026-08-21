using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using PointOfSale.Model.Services;

namespace PointOfSale.Infrastructure.Hashes;

public class ArgonPasswordHasher : IPasswordHasher
{
    private const int SaltSize = 32;
    private const int HashSize = 32;
    private const int Iterations = 4;
    private const int DegreeOfParallelism = 8;
    private const int MemorySize = 1024 * 1024;
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = HashPassword(password, salt);
        
        var combinedBytes = new byte[hash.Length + salt.Length];
        Array.Copy(salt, 0,  combinedBytes, 0, salt.Length);
        Array.Copy(hash, 0, combinedBytes, salt.Length, hash.Length);
        
        return Convert.ToBase64String(combinedBytes);
    }

    public bool Verify(string password, string hashedPassword)
    {
        byte[] combinedBytes = Convert.FromBase64String(hashedPassword);
        
        var salt = new byte[SaltSize];
        var hash = new byte[HashSize];
        Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
        Array.Copy(combinedBytes, SaltSize, hash, 0, HashSize);
        
        byte[] newHash =  HashPassword(password, salt);
        
        return CryptographicOperations.FixedTimeEquals(hash, newHash);
    }

    private static byte[] HashPassword(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize = MemorySize,
            Iterations = Iterations,
        };
        return argon2.GetBytes(HashSize);
    }
}