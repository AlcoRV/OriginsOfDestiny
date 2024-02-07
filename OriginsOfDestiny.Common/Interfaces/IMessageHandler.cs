using Telegram.Bot.Types;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Interfaces;

public interface IMessageHandler {
    public Task Handle(ITelegramBotClient botClient, Message message);
}
