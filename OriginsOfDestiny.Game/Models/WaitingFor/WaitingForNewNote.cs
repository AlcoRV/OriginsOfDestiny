using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.DataObjects.Models.Items.Pickups;
using OriginsOfDestiny.Game.Models.Actions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Game.Models.WaitingFor
{
    public class WaitingForNewNote : WaitingForBaseMessageHandler
    {
        private static readonly ResourceHelper<WaitingForNewNote> ResourceHelper = new();

        public WaitingForNewNote()
        {
            IgnoreCallbackQuery = false;
        }

        public override async Task Handle(Message message)
        {
            var data = message?.Text.Split(':');

            if(message == null)
            {
                await GameData.ClientData.EditMainMessageAsync(
                  caption: ResourceHelper.GetValue(Constants.RepeatOrCancel),
                  replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                 );
                return;
            }
            
            if (message.Text.Equals("-")) {
                GameData.ClientData.PlayerContext.ActiveItem = null;
                GameData.ClientData.WaitingForMessage = null;
            }
            else if(data.Length < 2 ) {
                await GameData.ClientData.EditMainMessageAsync(
                  caption: ResourceHelper.GetValue(Constants.RepeatOrCancel),
                  replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                 );
            }
            else
            {
                var caption = data[0].Trim();
                var description = string.Join(':', data.Skip(1)).Trim();

                var note = GameData.ClientData.PlayerContext.ActiveItem as Note;
                note.Name = caption;
                note.Description = description;
                note.Empty = false;

                await GameData.ClientData.EditMainMessageAsync(
                      caption: ResourceHelper.GetValue(Constants.Ok),
                      replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                     );

                GameData.ClientData.PlayerContext.ActiveItem = null;
                GameData.ClientData.WaitingForMessage = null;
            }

            Thread.Sleep( 3000 );
            await GameData.ClientData.BotClient.DeleteMessageAsync(GameData.ClientData.Id, message.MessageId);
        }

        public static class Constants
        {
            public static readonly string RepeatOrCancel = "REPEAT_OR_CANCEL";
            public static readonly string Ok = "OK";
        }
    }
}
