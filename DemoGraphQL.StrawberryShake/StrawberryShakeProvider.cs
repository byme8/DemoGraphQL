using Microsoft.Extensions.DependencyInjection;

namespace DemoGraphQL.StrawberryShake;

public class StrawberryShakeProvider
{
    // public static StrawberryShakeClient Create(HttpClient httpClient)
    // {
    //     var services = new ServiceCollection();
    //     services.AddStrawberryShakeClient();
    //     services.AddSingleton<IHttpClientFactory>(new FakeFactory(httpClient));
    //
    //     var provider = services.BuildServiceProvider();
    //     var client = provider.GetService<StrawberryShakeClient>()!;
    //
    //     return client;
    // }
    //
    // private class FakeFactory : IHttpClientFactory
    // {
    //     private readonly HttpClient client;
    //
    //     public FakeFactory(HttpClient client)
    //     {
    //         this.client = client;
    //     }
    //
    //     public HttpClient CreateClient(string name)
    //     {
    //         return client;
    //     }
    // }
}