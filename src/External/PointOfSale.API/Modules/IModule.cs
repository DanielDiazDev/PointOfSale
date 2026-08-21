namespace PointOfSale.API.Modules;

public interface IModule
{
    void MapEndpoints(IEndpointRouteBuilder app);
}