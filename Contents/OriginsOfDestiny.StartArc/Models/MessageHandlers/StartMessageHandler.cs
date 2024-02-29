using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers;

public class StartMessageHandler : IMessageHandler
{
    public async Task Handle(IGameData gameData, Message message)
    {
        await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, "Ты просыпаешься от прикосновения мягкой лапки к лицу...");

        await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, "Ещё один пришелец, причём совсем без магии.\r\n" +
                                                                        $"Топай отсюда, {message.From.FirstName}...");
    }
}
