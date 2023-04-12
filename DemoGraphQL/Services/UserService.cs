namespace DemoGraphQL.Services;

public record User(int Id, string Name, string Email);

public class UserService
{
    public UserService()
    {
        Users = Enumerable.Range(0, 100)
            .Select(o => new User(o, $"User Name {o}", $"User Email {o}"))
            .ToList();
    }

    private List<User> Users { get; set; }

    public async Task<User?> GetUser(int id)
    {
        Console.WriteLine($"GetUser({id})");
        return Users.FirstOrDefault(o => o.Id == id);
    }

    public async Task<IEnumerable<User>> GetUsers(int page, int size)
    {
        Console.WriteLine($"GetUsers({page}, {size})");
        return Users.Skip(page * size).Take(size);
    }

    public async Task<User> CreateUser(string name, string email)
    {
        Console.WriteLine($"CreateUser({name}, {email})");
        var newUser = new User(Users.Count, name, email);
        Users.Add(newUser);
        
        return newUser;
    }
}