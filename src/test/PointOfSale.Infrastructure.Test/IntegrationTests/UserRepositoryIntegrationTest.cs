using Microsoft.EntityFrameworkCore;
using PointOfSale.Infrastructure.Repositories;
using PointOfSale.Model.Users;

namespace PointOfSale.Infrastructure.Test.IntegrationTests;

[TestFixture]
public class UserRepositoryIntegrationTest
{
    private ApplicationDbContext _context;
    private UserRepository _userRepository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        _context = new ApplicationDbContext(options);
        _userRepository = new UserRepository(_context);
    }
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddAsync_WhenUserIsValid_ShouldPersistInDatabase()
    {
        var user = User.Create("Alice", "password", User.UserRole.Admin).Value;
        await _userRepository.AddAsync(user);
        var saved = await _userRepository.GetByIdAsync(user.Id);
        Assert.That(saved, Is.Not.Null);
        Assert.That(saved.Name, Is.EqualTo("Alice"));
    }
    [Test]
    public async Task GetByNameAsync_WhenUserExists_ShouldReturnUser()
    {
        var user = User.Create("Bob", "password123", User.UserRole.Vendor).Value;
        await _userRepository.AddAsync(user);

        var found = await _userRepository.GetByNameAsync("Bob");

        Assert.That(found, Is.Not.Null);
        Assert.That(found.Name, Is.EqualTo("Bob"));
    }

    [Test]
    public async Task UpdateAsync_WhenUserIsModified_ShouldPersistChanges()
    {
        var user = User.Create("Charlie", "password123", User.UserRole.Vendor).Value;
        await _userRepository.AddAsync(user);

        user.Update("Charles", "newPass123", User.UserRole.Supervisor);
        await _userRepository.UpdateAsync(user);

        var updated = await _userRepository.GetByIdAsync(user.Id);
        Assert.That(updated.Name, Is.EqualTo("Charles"));
        Assert.That(updated.Password, Is.EqualTo("newPass123"));
        Assert.That(updated.Role, Is.EqualTo(User.UserRole.Supervisor));
    }

    [Test]
    public async Task DeleteAsync_WhenUserExists_ShouldRemoveFromDatabase()
    {
        var user = User.Create("David", "password123", User.UserRole.Admin).Value;
        await _userRepository.AddAsync(user);

        await _userRepository.DeleteAsync(user.Id);

        var deleted = await _userRepository.GetByIdAsync(user.Id);
        Assert.That(deleted, Is.Null);
    }
}