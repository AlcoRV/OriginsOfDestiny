using Microsoft.Extensions.DependencyInjection;
using OriginsOfDestiny.Common.Handlers;
using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Providers;
using OriginsOfDestiny.Common.Test;

namespace OriginsOfDestiny.Common.Locators;

public class DIContainerLocator
{
    private readonly ServiceProvider _serviceProvider;

    public DIContainerLocator()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(new ConfigurationProvider().GetConfiguration());
        serviceCollection.AddSingleton<TelegramBotLocator>();
        serviceCollection.AddTransient<ITelegramUpdateHandler, TelegramUpdateHandler>();
        serviceCollection.AddTransient<ITelegramErrorHandler, TelegramErrorHandler>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public TestService GetTestService() => _serviceProvider.GetRequiredService<TestService>();

    public void RunTelegramBot() => _serviceProvider.GetRequiredService<TelegramBotLocator>().RunBot();
}
