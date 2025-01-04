using Telegram.Bot.Types;

namespace OriginsOfDestiny.Telegram
{
    public interface IMessageHandler
    {
        Task HandleMessage(Message message, CancellationToken token);
    }
}
