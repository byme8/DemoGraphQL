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
    
    public Role? GetRole(int id) => Roles.FirstOrDefault(o => o.Id == id);
    
    public IEnumerable<Role> GetRoles() => Roles;
    
    public IReadOnlyDictionary<int, Role[]> GetRolesByUserId(int[] ids)
    {
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