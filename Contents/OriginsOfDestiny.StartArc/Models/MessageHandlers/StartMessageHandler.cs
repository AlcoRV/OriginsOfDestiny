using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers
{
    using ArcConstants = Constants.Constants;
    using GameConstants = Game.Constants.Constants;

    public class StartMessageHandler : IMessageHandler
    {
        public async Task Handle(IGameData gameData, Message message)
        {
            var resourceHelper = new ResourceHelper<StartMessageHandler>();

            gameData.ClientData.RiddenMessagesCodes = new HashSet<string>();

            await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Out.WokeUp));
            
            using var fileStream = new FileManager().GetFileStream(GameConstants.Files.Pictures.Characters.Simon);

            var answer = await gameData.ClientData.BotClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: new InputFileStream(fileStream),
                    caption: resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Simon.OneMore),
                    replyMarkup: new InlineKeyboardMarkup(new[]
                {
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Me.Who), ArcConstants.Messages.SimonStart.Me.Who),
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Me.How), ArcConstants.Messages.SimonStart.Me.How),
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Me.Where), ArcConstants.Messages.SimonStart.Me.Where)
                }
                .Chunk(1))
                );

            gameData.ClientData.DefaultMessageHandler = new SimonStartDefaultMessageHandler();
            gameData.ClientData.MainMessageId = answer.MessageId;
        }
    }
}
