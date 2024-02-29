using Microsoft.Extensions.Configuration;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Locators;
using OriginsOfDestiny.Common.Options;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Locators;

public class TelegramBotLocator: ITelegramBotLocator
{
    private readonly TelegramBotClient _botClient;
    private readonly ITelegramUpdateHandler _updateHandler;
    private readonly ITelegramErrorHandler _errorHandler;

    public TelegramBotLocator(IConfiguration configuration, ITelegramUpdateHandler updateHandler, ITelegramErrorHandler errorHandler)
    {
        var settings = configuration.GetSection(TelegramBotOptions.Name).Get<TelegramBotOptions>() 
            ?? throw new NullReferenceException("Not found telegram bot settings!");

        _botClient = new TelegramBotClient(settings.Key);
        _updateHandler = updateHandler;
        _errorHandler = errorHandler;
    }

    public void RunBot()
    {
        _botClient.StartReceiving(_updateHandler.Update, _errorHandler.Error);

        Console.WriteLine("Bot runs!");
        Console.ReadKey();
    }
}
