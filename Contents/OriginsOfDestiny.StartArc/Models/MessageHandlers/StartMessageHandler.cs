using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.StartArc.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers
{
    using Constants = Constants.Constants;

    public class StartMessageHandler : IMessageHandler
    {
        public async Task Handle(IGameData gameData, Message message)
        {
            var resourceHelper = new ResourceHelper<StartMessageHandler>();

            await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, resourceHelper.GetValue(Constants.Messages.SimonStart.Out.WokeUp));

            using var fileStream = new FileManager().GetFileStream("simon.jpg");

            await gameData.ClientData.BotClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: new InputFileStream(fileStream),
                    caption: resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.OneMore),
                    replyMarkup: new InlineKeyboardMarkup(new[]
                {
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(Constants.Messages.SimonStart.Me.Who), Constants.Messages.SimonStart.Me.Who),
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(Constants.Messages.SimonStart.Me.How), Constants.Messages.SimonStart.Me.How),
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(Constants.Messages.SimonStart.Me.Where), Constants.Messages.SimonStart.Me.Where)
                }
                .Chunk(1))
                );

            gameData.ClientData.DefaultMessageHandler = new SimonStartDefaultMessageHandler();
        }
    }
}
