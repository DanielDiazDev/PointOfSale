using PointOfSale.Application.Branches;
using PointOfSale.Application.Users;
using PointOfSale.Application.Users.Create;
using PointOfSale.Application.Users.Delete;
using PointOfSale.Application.Users.Get;
using PointOfSale.Application.Users.Login;
using PointOfSale.Application.Users.Update;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Services;

namespace PointOfSale.API.Modules;

public class UsersModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users").WithTags("Users");

        group.MapGet("/", async (IUnitOfWork unitOfWork) =>
        {
            var getUsers = new GetUsers(unitOfWork);
            var result = await getUsers.Execute();
            return result;
        });

        group.MapPost("/register", async (IUnitOfWork unitOfWork, CreateUserCommand createUserCommand, IPasswordHasher passwordHasher) =>
        {
            var createUser = new CreateUser(unitOfWork, passwordHasher);
            var result = await createUser.Execute(createUserCommand);
            return result;
        });
        group.MapPost("/login", async (IUnitOfWork unitOfWork, LoginUserCommand loginUserCommand, IPasswordHasher passwordHasher, ITokenService tokenService) =>
        {
            var  loginUser = new LoginUser(unitOfWork, passwordHasher, tokenService);
            var result = await loginUser.Execute(loginUserCommand);
            return result;
         });

        group.MapGet("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var getUserById = new GetUserById(unitOfWork);
            var result = await getUserById.Execute(new GetUserByIdQuery(id));
            return result;
        });

        group.MapDelete("/{id}", async (IUnitOfWork unitOfWork, Guid id) =>
        {
            var deleteUser = new DeleteUser(unitOfWork);
            var result = await deleteUser.Execute(id);
            return result;
        });

        group.MapPut("/", async (IUnitOfWork unitOfWork, UpdateUserCommand updateUserCommand, IPasswordHasher passwordHasher) =>
        {
            var updateUser = new UpdateUser(unitOfWork, passwordHasher);
            var result = await updateUser.Execute(updateUserCommand);
            return result;
        });
    }
}