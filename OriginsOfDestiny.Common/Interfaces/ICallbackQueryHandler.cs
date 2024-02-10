using Telegram.Bot.Types;
using OriginsOfDestiny.Common.Models;

namespace OriginsOfDestiny.Common.Interfaces;

public interface ICallbackQueryHandler {
    public Task Handle(GameContext context, CallbackQuery callbackQuery);
}

