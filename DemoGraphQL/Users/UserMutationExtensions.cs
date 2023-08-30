using DemoGraphQL.Services;

namespace DemoGraphQL.Users;

[ExtendObjectType(typeof(Mutation))]
public class UserMutationExtensions
{
    public Task<User> CreateUser(string name, string email, [Service] UserService userService)
        => userService.CreateUser(name, email);
}