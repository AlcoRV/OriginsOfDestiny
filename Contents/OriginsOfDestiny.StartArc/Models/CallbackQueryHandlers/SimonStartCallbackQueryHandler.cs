using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers
{
    using Constants = Constants.Constants;

    public class SimonStartCallbackQueryHandler : ICallbackQueryHandler
    {
        public async Task Handle(IGameData gameData, CallbackQuery callbackQuery)
        {
            if(callbackQuery.Data.Equals(gameData.ClientData.LastCode, StringComparison.OrdinalIgnoreCase)) { return; }

            var resourceHelper = new ResourceHelper<SimonStartCallbackQueryHandler>();

            string message = null;
            if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.Who, StringComparison.OrdinalIgnoreCase))
            {
                message = resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Who);
            }
            else if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.How, StringComparison.OrdinalIgnoreCase))
            {
                message = resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.How);
            }
            else if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.Where, StringComparison.OrdinalIgnoreCase))
            {
                message = resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Where);
            }

            await gameData.ClientData.BotClient.EditMessageCaptionAsync(callbackQuery.Message!.Chat.Id,
                 callbackQuery.Message.MessageId,
                 message,
                 replyMarkup: new InlineKeyboardMarkup(new[]
                {
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(Constants.Messages.SimonStart.Me.Who), Constants.Messages.SimonStart.Me.Who),
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(Constants.Messages.SimonStart.Me.How), Constants.Messages.SimonStart.Me.How),
                InlineKeyboardButton.WithCallbackData(resourceHelper.GetValue(Constants.Messages.SimonStart.Me.Where), Constants.Messages.SimonStart.Me.Where)
                }
                .Chunk(1))
                );
        }
    }
}
