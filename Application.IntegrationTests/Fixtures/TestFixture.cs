using Application.IntegrationTests.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Fixtures;

[CollectionDefinition(nameof(TestFixture))]
public class TestFixtureCollection : ICollectionFixture<TestFixture> {}

public class TestFixture : IAsyncLifetime
{
    public static IServiceScopeFactory BaseScopeFactory;
    
    public Task InitializeAsync()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            EnvironmentName = "TestingEnv"
        });

        var services = builder.Services;
        
        // add any mock services here
        services.ReplaceServiceWithSingletonMock<IHttpContextAccessor>();

        var provider = services.BuildServiceProvider();
        BaseScopeFactory = provider.GetService<IServiceScopeFactory>()!;
        
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}