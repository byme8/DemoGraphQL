using DemoGraphQL.Services;
using DemoGraphQL.Users;
using HotChocolate.Resolvers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<UserQueryExtensions>()
    .AddTypeExtension<UserRolesQueryExtensions>();

services.AddSingleton<UserService>();
services.AddSingleton<RoleService>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(o => o.SwaggerDoc("v1", new() { Title = "DemoGraphQL", Version = "v1" }));

var app = builder.Build();

app.UseSwaggerUI();
app.UseRouting();
app.UseEndpoints(
    o =>
    {
        o.MapGraphQL();
        o.MapSwagger();
        o.MapGet("/users", 
            async (int page, int size, UserService service) => await service.GetUsers(page, size));
        
        o.MapGet("/users/{id}",
            async (int id, UserService service) => await service.GetUser(id));
        
        o.MapGet("/users/{id}/roles",
            async (int id, RoleService service) => await service.GetRolesByUserId(new[] { id }));
        
        o.MapPost("/users", 
            async (UserCreationRequest request, UserService service) => await service.CreateUser(request.Name, request.Name));
    });

app.Run();

public class Mutation
{
    public Task<User> CreateUser(string name, string email, [Service] UserService userService)
        => userService.CreateUser(name, email);
}

public class Query
{
    public DateTimeOffset Utc() => DateTimeOffset.UtcNow;

   
}

[ExtendObjectType(typeof(Query))]
public class UserQueryExtensions
{
    public Task<IEnumerable<User>> GetUsers(int page, int size, [Service] UserService userService)
        => userService.GetUsers(page, size);
    
    public Task<User?> GetUser(int id, [Service] UserService userService)
    => userService.GetUser(id);
}

[ExtendObjectType(typeof(User))]
public class UserRolesQueryExtensions
{
    public async Task<Role[]> GetRoles(
        IResolverContext context,
        [Parent] User user, [Service] RoleService roleService)
    {
        var a = context.Selection;
        return await context.BatchDataLoader<int, Role[]>(async (ids, ct)
            => await roleService.GetRolesByUserId(ids))
            .LoadAsync(user.Id);
    }
}