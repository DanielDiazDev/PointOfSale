using Microsoft.EntityFrameworkCore;
using PointOfSale.API.Extensions;
using PointOfSale.Infrastructure;
using PointOfSale.Model.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
builder.Services.AddOpenApi();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
// // //TODO: Buscar removerlo a futuro
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate(); 
// }
app.UseHttpsRedirection();

app.MapModules();

app.MapGet("/me", (ICurrentUser currentUser) =>
    {
        return Results.Ok(new
        {
            currentUser.IsAuthenticated,
            currentUser.UserId,
            currentUser.Username,
            currentUser.Role
        });
    })
    .RequireAuthorization();
app.Run();

