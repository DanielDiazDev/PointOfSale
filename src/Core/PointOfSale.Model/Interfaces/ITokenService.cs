namespace PointOfSale.Model.Repositories;

public interface ITokenService
{
    string GenerateToken(Guid id, string username, string role);
}