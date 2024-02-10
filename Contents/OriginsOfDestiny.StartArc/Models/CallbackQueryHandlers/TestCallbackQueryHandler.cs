using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers;

public class TestCallbackQueryHandler : ICallbackQueryHandler
{
    public async Task Handle(GameContext context, CallbackQuery callbackQuery)
    {
        await context.BotClient.SendTextMessageAsync(callbackQuery.From.Id, callbackQuery.Data!);
    }
}
