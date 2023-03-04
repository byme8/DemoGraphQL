using DemoGraphQL.Services;

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
        o.MapGet("/users", (int page, int size, UserService service) => service.GetUsers(page, size));
        o.MapGet("/users/{id}", (int id, UserService service) => service.GetUser(id));
        o.MapGet("/users/{id}/roles", (int id, RoleService service) => service.GetRolesByUserId(new[] { id }));
    });

app.Run();