using Telegram.Bot.Types;

namespace OriginsOfDestiny.Telegram
{
    public interface IComandHandler
    {
        Task HandleMessageUpdateAsync(Message message, CancellationToken token);

        Task HandleCallbackQueryUpdateAsync(CallbackQuery query, CancellationToken token);
    }
}
