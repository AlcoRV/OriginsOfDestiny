using Telegram.Bot.Types;

namespace OriginsOfDestiny.Telegram
{
    public interface ICallbackHandler
    {
        Task HandleCallback(CallbackQuery query, CancellationToken token);
    }
}
