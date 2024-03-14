using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.Game.Enums;
using Telegram.Bot;

namespace OriginsOfDestiny.StartArc.Models.WaitingForHandlers.Message
{
    using static OriginsOfDestiny.StartArc.Constants.Constants.Messages;

    public class WaitingForNameMessageHandler : WaitingForBaseMessageHandler
    {
        private ResourceHelper<WaitingForNameMessageHandler> _resourceHelper { get; set; } = new();

        public override async Task Handle(Telegram.Bot.Types.Message message)
        {
            string replyCode;

            if(message.Text.Any(c => !char.IsLetterOrDigit(c)))
            {
                if (!GameData.ClientData.RiddenMessagesCodes.Contains(SimonStart.Simon.FoolTry))
                {
                    replyCode = SimonStart.Simon.FoolTry;
                    (GameData.ClientData.RiddenMessagesCodes as HashSet<string>).Add(replyCode);
                }
                else
                {
                    GameData.ClientData.WaitingForMessage = null;
                    replyCode = SimonStart.Simon.NotNeedName;
                }
            }
            else
            {
                GameData.ClientData.WaitingForMessage = null;
                GameData.ClientData.PlayerContext.MainHero.Name = message.Text;
                replyCode = SimonStart.Simon.SeeLater;
            }

            await GameData.ClientData.BotClient.EditMessageCaptionAsync(message.From!.Id,
                GameData.ClientData.MainMessageId,
                 GetMessageByReplyCode(replyCode, GameData.ClientData.PlayerContext.MainHero.Gender)
                );

            if (replyCode.Equals(SimonStart.Simon.NotNeedName))
            {
                Thread.Sleep(5000);

                await GameData.ClientData.BotClient.EditMessageCaptionAsync(message.From!.Id,
                    GameData.ClientData.MainMessageId,
                 _resourceHelper.GetValue(SimonStart.Simon.GetLost)
                );

                GameData.ClientData.PlayerContext.MainHero.Name = _resourceHelper.GetValue(GameData.ClientData.PlayerContext.MainHero.Gender == Game.Enums.Gender.Man
                                                                                                ? SimonStart.General.Bastard
                                                                                                : SimonStart.General.FoolW);
            }

            if (!replyCode.Equals(SimonStart.Simon.FoolTry))
            {
                await ExitDialog(GameData);
            }

            Thread.Sleep(3000);
            await GameData.ClientData.BotClient.DeleteMessageAsync(message.From!.Id, message.MessageId);
        }

        private string GetMessageByReplyCode(string replyCode, Gender gender)
        {
            var message = _resourceHelper.GetValue(replyCode);

            if (replyCode.Equals(SimonStart.Simon.FoolTry))
            {
                message = string.Format(message, gender == Gender.Man
                                                ? _resourceHelper.GetValue(SimonStart.General.TryPartM)
                                                : _resourceHelper.GetValue(SimonStart.General.TryPartW));
            }
            else if(replyCode.Equals(SimonStart.Simon.NotNeedName))
            {
                message = string.Format(message, gender == Gender.Man
                                                ? _resourceHelper.GetValue(SimonStart.General.Bastard)
                                                : _resourceHelper.GetValue(SimonStart.General.FoolW));
            }

            return message;
        }

        private async Task ExitDialog(IGameData gameData)
        {
            // to the world!
        }
    }
}
