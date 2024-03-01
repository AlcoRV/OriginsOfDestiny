using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers;

public class StartMessageHandler : IMessageHandler
{
    public async Task Handle(IGameData gameData, Message message)
    {
        await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, "Ты просыпаешься от прикосновения мягкой лапки к лицу...");

        var file = System.IO.File.Open(Directory.GetParent(AppContext.BaseDirectory)!.FullName + "/wwwroot/simon.jpg", FileMode.Open);

        await gameData.ClientData.BotClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: new InputFileStream(file),
                caption: "Симон: \"Ещё один пришелец, причём совсем без магии.\"",
                replyMarkup: new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Ты кто?", "who"),
                InlineKeyboardButton.WithCallbackData("Как я здесь оказался?", "how"),
                InlineKeyboardButton.WithCallbackData("Что это за место?", "where")
            }
            .Chunk(1))
            );

        gameData.ClientData.DefaultMessageHandler = new SimonStartDefaultMessageHandler();
    }
}
