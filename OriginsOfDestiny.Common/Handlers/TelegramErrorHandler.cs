using OriginsOfDestiny.Common.Interfaces;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Handlers;

public class TelegramErrorHandler : ITelegramErrorHandler
{
    public async Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
    {
        Console.WriteLine(exception.Message);
    }
}
