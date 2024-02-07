using OriginsOfDestiny.Common.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Common.Test;

public class TestCallbackQueryHandle : ICallbackQueryHandler
{
    public async Task Handle(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        await botClient.SendTextMessageAsync(callbackQuery.From!.Id, callbackQuery.Data!,
            replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Showed message", "Hidden message") }));
    }
}
