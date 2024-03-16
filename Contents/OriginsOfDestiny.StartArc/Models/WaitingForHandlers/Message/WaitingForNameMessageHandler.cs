using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Managers;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Entity;
using OriginsOfDestiny.Game.Extentions;
using OriginsOfDestiny.Game.Models.Actions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.WaitingForHandlers.Message
{
    using static OriginsOfDestiny.StartArc.Constants.Constants.Messages;
    using GameConstants = Data.Constants.DConstants;

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
                GameData.ClientData.PlayerContext.Hero.Name = message.Text;
                replyCode = SimonStart.Simon.SeeLater;
            }

            await GameData.ClientData.BotClient.EditMessageCaptionAsync(message.From!.Id,
                GameData.ClientData.MainMessage.MessageId,
                 GetMessageByReplyCode(replyCode, GameData.ClientData.PlayerContext.Hero)
                );

            if (replyCode.Equals(SimonStart.Simon.NotNeedName))
            {
                Thread.Sleep(5000);

                await GameData.ClientData.BotClient.EditMessageCaptionAsync(message.From!.Id,
                    GameData.ClientData.MainMessage.MessageId,
                 _resourceHelper.GetValue(SimonStart.Simon.GetLost)
                );

                GameData.ClientData.PlayerContext.Hero.Name = _resourceHelper.GetValue(GameData.ClientData.PlayerContext.Hero.Gender == Gender.Man
                                                                                                ? SimonStart.General.Bastard
                                                                                                : SimonStart.General.FoolW);
            }

            if (!replyCode.Equals(SimonStart.Simon.FoolTry))
            {
                await GameData.ClientData.BotClient.SendTextMessageAsync(message.Chat!.Id,
                    string.Format(_resourceHelper.GetValue(SimonStart.Out.Named), GameData.ClientData.PlayerContext.Hero.Name)
                );

                await ExitDialog(message);
            }

            Thread.Sleep(3000);
            await GameData.ClientData.BotClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
        }

        private string GetMessageByReplyCode(string replyCode, Hero hero)
        {
            var message = _resourceHelper.GetValue(replyCode);

            if (replyCode.Equals(SimonStart.Simon.FoolTry))
            {
                message = string.Format(message, hero.Gender == Gender.Man
                                                ? _resourceHelper.GetValue(SimonStart.General.TryPartM)
                                                : _resourceHelper.GetValue(SimonStart.General.TryPartW));
            }
            else if(replyCode.Equals(SimonStart.Simon.NotNeedName))
            {
                message = string.Format(message, hero.Gender == Gender.Man
                                                ? _resourceHelper.GetValue(SimonStart.General.Bastard)
                                                : _resourceHelper.GetValue(SimonStart.General.FoolW));
            }
            else if (replyCode.Equals(SimonStart.Simon.SeeLater))
            {
                message = string.Format(message, hero.Name);
            }

            return message;
        }

        private async Task ExitDialog(Telegram.Bot.Types.Message message)
        {
            await GameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id,
                _resourceHelper.GetValue(SimonStart.Out.Disappear));

            using var fileStream = new FileManager().GetFileStream(GameConstants.Files.Pictures.Locations.EAForest);

            var answer = await GameData.ClientData.BotClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: new InputFileStream(fileStream),
                    caption: _resourceHelper.GetValue(SimonStart.Out.EAF),
                    replyMarkup: new InlineKeyboardMarkup(
                        (HeroActions.GetBaseActions())
                        .Chunk(1))
                    );

            GameData.ClientData.Clear();
            GameData.ClientData.MainMessage = answer;
        }
    }
}
