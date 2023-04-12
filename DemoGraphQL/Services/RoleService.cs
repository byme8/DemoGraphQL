namespace DemoGraphQL.Services;

public record Role(int Id, string Name);

public class RoleService
{
    public RoleService()
    {
        Roles = new Role[]
        {
            new(0, "Admin"),
            new(1, "Sales Manager"),
            new(2, "Car Manager"),
            new(3, "Map Manager"),
            new(4, "Office Manager"),
        };
    }

    private Role[] Roles { get; }

    public async Task<Role?> GetRole(int id)
    {
        Console.WriteLine($"GetRole({id})");
        return Roles.FirstOrDefault(o => o.Id == id);
    }

    public async Task<IEnumerable<Role>> GetRoles()
    {
        Console.WriteLine($"GetRoles()");
        return Roles;
    }

    public async Task<IReadOnlyDictionary<int, Role[]>> GetRolesByUserId(IReadOnlyList<int> ids)
    {
        Console.WriteLine($"GetRolesByUserId({string.Join(", ", ids)})");
        var result = new Dictionary<int, Role[]>();
        foreach (var id in ids)
        {
            var random = new Random(id);
            var roles = Roles
                .Where(o => random.Next(0, 2) == 1)
                .ToArray();

            result.Add(id, roles);
        }

        return result;
    }
}