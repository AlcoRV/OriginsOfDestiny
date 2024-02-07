using Microsoft.Extensions.DependencyInjection;
using OriginsOfDestiny.Common.Providers;
using OriginsOfDestiny.Common.Services;

namespace OriginsOfDestiny.Common.Locators;

public class DIContainerLocator
{
    private readonly ServiceProvider _serviceProvider;

    public DIContainerLocator()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(new ConfigurationProvider().GetConfiguration());
        serviceCollection.AddScoped<TestService>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public TestService GetTestService() => _serviceProvider.GetRequiredService<TestService>();
}
