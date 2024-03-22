using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.Common.UI;
using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.StartArc.Models.WaitingForHandlers.Message;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers
{
    using Constants = Constants.Constants;

    public class SimonStartCallbackQueryHandler : ICallbackQueryHandler
    {
        private ResourceHelper<SimonStartCallbackQueryHandler> _resourceHelper;
        public async Task Handle(IGameData gameData, CallbackQuery callbackQuery)
        {
            if (callbackQuery.Data.Equals(gameData.ClientData.LastCode, StringComparison.OrdinalIgnoreCase)) { return; }

            _resourceHelper = new ResourceHelper<SimonStartCallbackQueryHandler>();

            if (new[] { Constants.Messages.SimonStart.Me.Who ,
                    Constants.Messages.SimonStart.Me.How,
                    Constants.Messages.SimonStart.Me.Where}.Contains(callbackQuery.Data))
            {
                await HandleMainMessages(gameData, callbackQuery);
            }

            if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.Leave, StringComparison.OrdinalIgnoreCase))
            {
                await HandleLeaveMessage(gameData, callbackQuery);
            }

            if(callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.NotGirl, StringComparison.OrdinalIgnoreCase)
                || callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.What, StringComparison.OrdinalIgnoreCase)) {
                await HandleLastMessage(gameData, callbackQuery);
            }

        }

        private async Task HandleMainMessages(IGameData gameData, CallbackQuery callbackQuery)
        {
            var riddenCodes = gameData.ClientData.RiddenMessagesCodes as HashSet<string>;
            string message;
            if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.Who, StringComparison.OrdinalIgnoreCase))
            {
                message = _resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Who);
                if (!gameData.ClientData.RiddenMessagesCodes.Contains(Constants.Messages.SimonStart.Me.Who))
                {
                    riddenCodes.Add(Constants.Messages.SimonStart.Me.Who);
                }
            }
            else if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.How, StringComparison.OrdinalIgnoreCase))
            {
                message = _resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.How);
                if (!gameData.ClientData.RiddenMessagesCodes.Contains(Constants.Messages.SimonStart.Me.How))
                {
                    riddenCodes.Add(Constants.Messages.SimonStart.Me.How);
                }
            }
            else if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.Where, StringComparison.OrdinalIgnoreCase))
            {
                message = _resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Where);
                if (!gameData.ClientData.RiddenMessagesCodes.Contains(Constants.Messages.SimonStart.Me.Where))
                {
                    riddenCodes.Add(Constants.Messages.SimonStart.Me.Where);
                }
            }
            else
            {
                return;
            }

            var buttons = new List<InlineKeyboardButton>()
                {
                UITools.GetButton<SimonStartCallbackQueryHandler>(Constants.Messages.SimonStart.Me.Who),
                UITools.GetButton<SimonStartCallbackQueryHandler>(Constants.Messages.SimonStart.Me.How),
                UITools.GetButton<SimonStartCallbackQueryHandler>(Constants.Messages.SimonStart.Me.Where)
                };

            if(gameData.ClientData.RiddenMessagesCodes.Count() == 3)
            {
                buttons.Add(UITools.GetButton<SimonStartCallbackQueryHandler>(Constants.Messages.SimonStart.Me.Leave));
            }

            await gameData.ClientData.EditMainMessageAsync(
                caption: message,
                replyMarkup: new InlineKeyboardMarkup(buttons.Chunk(1))
                );
        }

        private async Task HandleLeaveMessage(IGameData gameData, CallbackQuery callbackQuery)
        {
            var message = _resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Stop);
            var buttons = new List<InlineKeyboardButton>
                {
                    UITools.GetButton<SimonStartCallbackQueryHandler>(Constants.Messages.SimonStart.Me.NotGirl)
                };

            await gameData.ClientData.EditMainMessageAsync(
                caption: message,
                replyMarkup: new InlineKeyboardMarkup(buttons.Chunk(1))
                );

            gameData.ClientData.WaitingForMessage = WaitingForBaseMessageHandler.Factory.Create<WaitingForNameMessageHandler>(gameData);
        }

        private async Task HandleLastMessage(IGameData gameData, CallbackQuery callbackQuery)
        {
            string message = null;
            if (callbackQuery.Data.Equals(Constants.Messages.SimonStart.Me.NotGirl, StringComparison.OrdinalIgnoreCase))
            {
                message = _resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.SorrySayName);

                gameData.ClientData.PlayerContext.Hero.Gender = Gender.Man;
            }

            await gameData.ClientData.EditMainMessageAsync(caption: message);

            gameData.ClientData.WaitingForMessage = WaitingForBaseMessageHandler.Factory.Create<WaitingForNameMessageHandler>(gameData);
        }
    }
}
