using Microsoft.Extensions.DependencyInjection;
using OriginsOfDestiny.Common.Providers;
using OriginsOfDestiny.Common.Services;

namespace OriginsOfDestiny.Common.Managers;

public class DIContainerManager
{
    public ServiceProvider ServiceProvider { get; }

    public DIContainerManager()
    {
        ServiceProvider = new ServiceCollection()
        .AddSingleton(new ConfigurationProvider().GetConfiguration())
        .AddScoped<TestService>()
        .BuildServiceProvider();
    }

    public TestService GetTestService() => ServiceProvider.GetRequiredService<TestService>();
}
