using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.Storage;
using OriginsOfDestiny.Common.Models.WaitingFor;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.WaitingForHandlers.Message
{
    using Constants = Constants.Constants;

    public class WaitingForNameMessageHandler : WaitingForBaseMessageHandler
    {
        public override async Task Handle(Telegram.Bot.Types.Message message)
        {
            var _resourceHelper = new ResourceHelper<WaitingForNameMessageHandler>();
            string replyCode;

            if(message.Text.Any(c => !char.IsLetterOrDigit(c)))
            {
                if (!GameData.ClientData.RiddenMessagesCodes.Contains(Constants.Messages.SimonStart.Simon.FoolTry))
                {
                    replyCode = Constants.Messages.SimonStart.Simon.FoolTry;
                    (GameData.ClientData.RiddenMessagesCodes as HashSet<string>).Add(replyCode);
                }
                else
                {
                    GameData.ClientData.WaitingForMessage = null;
                    replyCode = Constants.Messages.SimonStart.Simon.NotNeedName;
                }
            }
            else
            {
                GameData.ClientData.WaitingForMessage = null;
                GameData.ClientData.PlayerContext.MainHero.Name = message.Text;
                replyCode = Constants.Messages.SimonStart.Simon.SeeLater;
            }

            await GameData.ClientData.BotClient.SendTextMessageAsync(message.From!.Id,
                 _resourceHelper.GetValue(replyCode)
                );

            if (replyCode.Equals(Constants.Messages.SimonStart.Simon.NotNeedName))
            {
                await GameData.ClientData.BotClient.SendTextMessageAsync(message.From!.Id,
                 _resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.GetLost)
                );

                GameData.ClientData.PlayerContext.MainHero.Name = _resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Bastard).Split("\"")[1].Replace("!", "");
            }

            if (!replyCode.Equals(Constants.Messages.SimonStart.Simon.FoolTry))
            {
                await ExitDialog(GameData);
            }
        }

        private async Task ExitDialog(IGameData gameData)
        {
            // to the world!
        }
    }
}
