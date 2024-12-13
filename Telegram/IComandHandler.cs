using Telegram.Bot.Types;

namespace OriginsOfDestiny.Telegram
{
    public interface IComandHandler
    {
        Task HandleUpdateAsync(Message message, CancellationToken token);
    }
}
