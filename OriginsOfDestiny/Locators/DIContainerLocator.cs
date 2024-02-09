using Microsoft.Extensions.DependencyInjection;
using OriginsOfDestiny.Common.Handlers;
using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Locators;
using OriginsOfDestiny.Common.Models;
using OriginsOfDestiny.Common.Providers;
using OriginsOfDestiny.Common.Test;
using OriginsOfDestiny.Handlers;

namespace OriginsOfDestiny.Locators;

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
        serviceCollection.AddScoped<GameContext>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public TestService GetTestService() => _serviceProvider.GetRequiredService<TestService>();

    public void RunTelegramBot() => _serviceProvider.GetRequiredService<TelegramBotLocator>().RunBot();
}
