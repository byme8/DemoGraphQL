using DemoGraphQL.Services;
using DemoGraphQL.Users;

namespace DemoGraphQL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddTypeExtension<UserQueryExtensions>()
            .AddTypeExtension<UserRolesQueryExtensions>()
            .AddTypeExtension<UserMutationExtensions>();

        services.AddSingleton<UserService>();
        services.AddSingleton<RoleService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o => o.SwaggerDoc("v1", new() { Title = "DemoGraphQL", Version = "v1" }));

        var app = builder.Build();

        app.UseSwaggerUI();
        app.UseRouting();
        app.MapGraphQL();
        app.MapSwagger();
        app.MapGet("/users",
            async (int page, int size, UserService service) => await service.GetUsers(page, size));

        app.MapGet("/users/{id}",
            async (int id, UserService service) => await service.GetUser(id));

        app.MapGet("/users/{id}/roles",
            async (int id, RoleService service) => await service.GetRolesByUserId(new[] { id }));

        app.MapPost("/users",
            async (UserCreationRequest request, UserService service) =>
                await service.CreateUser(request.Name, request.Email));

        app.Run();
    }
}

public class Mutation
{
}

public class Query
{
}