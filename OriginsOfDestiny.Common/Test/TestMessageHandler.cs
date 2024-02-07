using OriginsOfDestiny.Common.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Common.Test;

public class TestMessageHandler : IMessageHandler
{
    public async Task Handle(ITelegramBotClient botClient, Message message)
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, message.Text!,
            replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[] { InlineKeyboardButton.WithCallbackData("Showed message", "Hidden message") }));
    }
}
