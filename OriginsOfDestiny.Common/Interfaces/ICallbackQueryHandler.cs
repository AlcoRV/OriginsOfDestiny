using Telegram.Bot.Types;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Interfaces;

public interface ICallbackQueryHandler {
    public Task Handle(ITelegramBotClient botClient, CallbackQuery callbackQuery);
}

