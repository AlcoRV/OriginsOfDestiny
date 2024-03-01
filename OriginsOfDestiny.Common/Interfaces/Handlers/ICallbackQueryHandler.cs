using Telegram.Bot.Types;
using OriginsOfDestiny.Common.Interfaces.Storages;

namespace OriginsOfDestiny.Common.Interfaces.Handlers;

public interface ICallbackQueryHandler
{
    public Task Handle(IGameData gameData, CallbackQuery callbackQuery);
}

