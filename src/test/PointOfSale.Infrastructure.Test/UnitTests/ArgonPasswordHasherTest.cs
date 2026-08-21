using PointOfSale.Infrastructure.Hashes;

namespace PointOfSale.Infrastructure.Test.UnitTests;

[TestFixture]
public class ArgonPasswordHasherTest
{
    private ArgonPasswordHasher _hasher;

    [SetUp]
    public void SetUp()
    {
        _hasher = new ArgonPasswordHasher();
    }

    [Test]
    public void Hash_WhenPasswordIsValid_ShouldReturnNonEmptyString()
    {
        var password = "securePassword123";
        var hashed =  _hasher.Hash(password);
        Assert.That(hashed, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Verify_WhenPasswordMatches_ShouldReturnTrue()
    {
        var password = "securePassword123";
        var hashed = _hasher.Hash(password);
        var result =  _hasher.Verify(password, hashed);
        Assert.That(result, Is.True);
    }
    [Test]
    public void Verify_WhenPasswordDoesNotMatch_ShouldReturnFalse()
    {
        var password = "securePassword123";
        var hashed = _hasher.Hash(password);

        var result = _hasher.Verify("wrongPassword", hashed);

        Assert.That(result, Is.False);
    }


    [Test]
    public void Hash_WhenCalledTwiceWithSamePassword_ShouldReturnDifferentHashes()
    {
        var password = "securePassword123";

        var hash1 = _hasher.Hash(password);
        var hash2 = _hasher.Hash(password);

        Assert.That(hash1, Is.Not.EqualTo(hash2));
    }
}