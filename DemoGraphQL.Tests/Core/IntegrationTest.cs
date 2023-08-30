namespace DemoGraphQL.Tests.Core;

public class IntegrationTest : IAsyncLifetime
{
    static IntegrationTest()
    {
        Factory = WebApis.Create();
    }

    public static TestWebApplicationFactory Factory { get; set; }
    
    public HttpClient Client { get; } = Factory.CreateClient();
    
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Client.Dispose();
        
        return Task.CompletedTask;
    }
}