using OriginsOfDestiny.Common.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Common.Handlers;

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        await botClient.SendTextMessageAsync(update.Message!.Chat.Id, update.Message.Text!);
    }
}
