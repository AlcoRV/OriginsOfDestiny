using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers;

public class SimonStartDefaultMessageHandler : IMessageHandler
{
    public async Task Handle(IGameData gameData, Message message)
    {
        var messageText = "";
        switch (new Random().Next(3))
        {
            case 0: messageText = "Симон: \"Ты дурак?😡\""; break;
            case 1: messageText = "Симон: \"Спокойнее😡\""; break;
            case 2: messageText = "Симон: \"Убить тебя будет не жалко😡\""; break;

            default: break;
        }

        var messageId = (await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, messageText)).MessageId;

        Thread.Sleep(2000);

        await gameData.ClientData.BotClient.DeleteMessageAsync(message.Chat.Id, messageId);
        await gameData.ClientData.BotClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
    }
}
