namespace DemoGraphQL.Tests.Core;

public abstract class WebApis
{
    public static TestWebApplicationFactory Create()
        => new();
}