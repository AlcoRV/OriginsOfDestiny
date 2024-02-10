using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers;

public class TestMessageHandler : IMessageHandler
{
    public async Task Handle(GameContext context, Message message)
    {
        await context.BotClient.SendTextMessageAsync(message.Chat.Id, message.Text!);
    }
}
