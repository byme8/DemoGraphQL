namespace DemoGraphQL.Services;

public record User(int Id, string Name, string Email);

public class UserService
{
    public UserService()
    {
        Users = Enumerable.Range(0, 100)
            .Select(o => new User(o, $"User Name {o}", $"User Email {o}"))
            .ToArray();
    }

    private User[] Users { get; set; }
    
    public async Task<User?> GetUser(int id) => Users.FirstOrDefault(o => o.Id == id);
    
    public async Task<IEnumerable<User>> GetUsers(int page, int size) => Users.Skip(page * size).Take(size);
}