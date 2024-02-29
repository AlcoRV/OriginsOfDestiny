using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers;

public class TestCallbackQueryHandler : ICallbackQueryHandler
{
    public async Task Handle(IGameData gameData, CallbackQuery callbackQuery)
    {
        await gameData.ClientData.BotClient.SendTextMessageAsync(callbackQuery.From.Id, callbackQuery.Data!);
    }
}
