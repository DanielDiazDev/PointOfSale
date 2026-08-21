using PointOfSale.API.Modules;

namespace PointOfSale.API.Extensions;

public static class ModuleExtensions
{
    public static void MapModules(this WebApplication app)
    {
        var modules = typeof(Program).Assembly
            .GetTypes()
            .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
        foreach (var module in modules)
        {
            module.MapEndpoints(app);
        }
    }
}