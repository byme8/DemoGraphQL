using DemoGraphQL.Services;
using DemoGraphQL.Users;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

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