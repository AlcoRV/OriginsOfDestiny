using OriginsOfDestiny.Models.Characters;
using OriginsOfDestiny.Telegram;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Services
{
    public interface IMenuService : ICallbackHandler
    {
        Task ShowMainMenu(long chatId, CancellationToken token);
    }
}
