using Microsoft.Extensions.Configuration;
using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Properties.Settings;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Locators;

public class TelegramBotLocator: ITelegramBotLocator
{
    private readonly TelegramBotClient _botClient;
    private readonly ITelegramUpdateHandler _updateHandler;
    private readonly ITelegramErrorHandler _errorHandler;

    public TelegramBotLocator(IConfiguration configuration, ITelegramUpdateHandler updateHandler, ITelegramErrorHandler errorHandler)
    {
        var settings = configuration.GetSection(TelegramBotSettings.Name).Get<TelegramBotSettings>() 
            ?? throw new NullReferenceException("Don't found telegram bot settings!");

        _botClient = new TelegramBotClient(settings.Key);
        _updateHandler = updateHandler;
        _errorHandler = errorHandler;
    }

    public void RunBot()
    {
        _botClient.StartReceiving(_updateHandler.Update, _errorHandler.Error);

        Console.ReadKey();
    }
}
