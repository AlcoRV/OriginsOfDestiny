using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers;

public class SimonStartCallbackQueryHandler : ICallbackQueryHandler
{
    public async Task Handle(IGameData gameData, CallbackQuery callbackQuery)
    {
        string message = null;
        if(callbackQuery.Data.Equals("who", StringComparison.OrdinalIgnoreCase))
        {
            message = "Симон: \"М-да, ты совсем безнадёжен...\r\n" +
                "Я Симон, дух стихий. Впрочем, тебе этого достаточно\"";
        }
        else if (callbackQuery.Data.Equals("how", StringComparison.OrdinalIgnoreCase))
        {
            message = "Симон: \"Чёрт его знает, откуда вы все здесь берётесь. У каждого отшибает память. Лишь ходят, землю сотрясают, житья не дают.\"";
        }
        else if (callbackQuery.Data.Equals("where", StringComparison.OrdinalIgnoreCase))
        {
            message = "Симон: \"Это \"Вечно-осенний лес\", можешь догадаться, почему он так называется. Здесь всё в гармонии, не смей нарушать этого!\"";
        }

        await gameData.ClientData.BotClient.EditMessageCaptionAsync(callbackQuery.Message!.Chat.Id,
             callbackQuery.Message.MessageId,
             message,
            replyMarkup: new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Ты кто?", "who"),
                InlineKeyboardButton.WithCallbackData("Как я здесь оказался?", "how"),
                InlineKeyboardButton.WithCallbackData("Что это за место?", "where")
            }
            .Chunk(1))
            ); 
    }
}
