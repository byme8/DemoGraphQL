using DemoGraphQL.Services;

namespace DemoGraphQL.Users;

[ExtendObjectType(typeof(Query))]
public class UserQueryExtensions
{
    public Task<IEnumerable<User>> GetUsers(int page, int size, [Service] UserService userService)
        => userService.GetUsers(page, size);

    public Task<User?> GetUser(int id, [Service] UserService userService)
        => userService.GetUser(id);
}