using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PointOfSale.Infrastructure.Auth;
using PointOfSale.Infrastructure.Hashes;
using PointOfSale.Infrastructure.Repositories;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Services;
namespace PointOfSale.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("Database"), new MariaDbServerVersion(new Version(10, 4, 28))));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IGenericRepository<,>),  typeof(GenericRepository<,>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, ArgonPasswordHasher>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUser, CurrentUser>();
        var key = configuration["JWT:Key"];
        var issuer = configuration["JWT:Issuer"];
        var audience = configuration["JWT:Audience"];
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly",
                policy => policy.RequireRole("Admin"));
        });
        return services;
    }
}
