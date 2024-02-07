using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Handlers;

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var manager = new TelegramUpdateHandlerManager();
        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            await manager.GetMessageHandler(update.Message!.Text!).Handle(botClient, update.Message);
        }
        else if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            await manager.GetCallbackQueryHandler(update.CallbackQuery.Data!).Handle(botClient, update.CallbackQuery);
        }

    }
}
