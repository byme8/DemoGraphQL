using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace DemoGraphQL.Tests.Core;

public class TestWebApplicationFactory : WebApplicationFactory<DemoGraphQL.Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);
        return host;
    }
}