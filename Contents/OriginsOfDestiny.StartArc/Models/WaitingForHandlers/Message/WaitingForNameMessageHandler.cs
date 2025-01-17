﻿using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Models.Entity;
using OriginsOfDestiny.Game.Models.Actions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

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
                GameData.ClientData.PlayerContext.Hero.Name = message.Text;
                replyCode = SimonStart.Simon.SeeLater;
            }

            await GameData.ClientData.EditMainMessageAsync(
                caption: GetMessageByReplyCode(replyCode, GameData.ClientData.PlayerContext.Hero)
                );

            if (replyCode.Equals(SimonStart.Simon.NotNeedName))
            {
                Thread.Sleep(5000);

                await GameData.ClientData.EditMainMessageAsync(
                    caption: _resourceHelper.GetValue(SimonStart.Simon.GetLost)
                    );

                GameData.ClientData.PlayerContext.Hero.Name = _resourceHelper.GetValue(GameData.ClientData.PlayerContext.Hero.Gender == Gender.Man
                                                                                                ? SimonStart.General.Bastard
                                                                                                : SimonStart.General.FoolW);
            }

            if (!replyCode.Equals(SimonStart.Simon.FoolTry))
            {
                await GameData.ClientData.SendMessageAsync(
                    string.Format(_resourceHelper.GetValue(SimonStart.Out.Named), GameData.ClientData.PlayerContext.Hero.Name));

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
            await GameData.ClientData.SendMessageAsync(
                    string.Format(_resourceHelper.GetValue(SimonStart.Out.Disappear)));

            GameData.ClientData.Clear();
            GameData.ClientData.PlayerContext.Opponent = null;

            await GameData.ClientData.SendPhotoAsync(
                    caption: _resourceHelper.GetValue(SimonStart.Out.EAF),
                    replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                    );
        }
    }
}
