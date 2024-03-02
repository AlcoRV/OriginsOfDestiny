using Microsoft.Extensions.DependencyInjection;
using OriginsOfDestiny.Common.Handlers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Common.Locators;
using OriginsOfDestiny.Common.Managers;
using OriginsOfDestiny.Common.Providers;
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
        serviceCollection.AddScoped<IClientDataManager, ClientDataManager>();
        serviceCollection.AddSingleton<ITimerHandler, TimerHandler>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public void RunTelegramBot() => _serviceProvider.GetRequiredService<TelegramBotLocator>().RunBot();
}
