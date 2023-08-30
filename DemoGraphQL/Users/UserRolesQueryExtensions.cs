using DemoGraphQL.Services;
using HotChocolate.Resolvers;

namespace DemoGraphQL.Users;

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