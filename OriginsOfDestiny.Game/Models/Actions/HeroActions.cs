using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using Stream = OriginsOfDestiny.DataObjects.Models.Items.InteractiveItems.Stream;
using OriginsOfDestiny.Game.Constants;
using OriginsOfDestiny.Common.UI;
using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Models.Entity;
using OriginsOfDestiny.DataObjects.Models.Items.Pickups;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class HeroActions
    {
        private readonly IGameData _gameData;
        private readonly Hero _hero;
        private static readonly ResourceHelper<HeroActions> ResourceHelper = new();

        public HeroActions(IGameData gameData)
        {
            _gameData = gameData;
            _hero = gameData.ClientData.PlayerContext.Hero;
        }

        public async Task LookAround()
        {
            var area = _gameData.ClientData.PlayerContext.Area;

            var sb = new StringBuilder();
            sb.AppendLine(area.Name);
            sb.AppendLine(area.Description);
            sb.AppendLine(ResourceHelper.GetValue(GConstants.Messages.Out.WeSee));
            foreach (var iItem in area.InteractiveItems)
            {
                sb.AppendLine("🔹 " + iItem.Name);
            }
            var textMessage = sb.ToString();

            var textButtons = new HashSet<InlineKeyboardButton>();
            foreach (var iItem in area.InteractiveItems)
            {

                if (iItem is Stream stream)
                {
                    textButtons.Add(UITools.GetButton<Stream>(IInteractiveItem.Messages.Interact, "_", nameof(Stream), stream.Id.ToString() ));
                }
                else if (iItem is Hollow hollow)
                {
                    textButtons.Add(UITools.GetButton<Hollow>(IInteractiveItem.Messages.Interact, "_", nameof(Hollow), hollow.Id.ToString() ));
                }
            }

            var buttons = new List<IEnumerable<InlineKeyboardButton>>();

            buttons.AddRange(textButtons.Chunk(1));
            buttons.Add(GetPersonalActions());

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: textMessage,
                 replyMarkup: new InlineKeyboardMarkup(buttons)
                );
        }

        public async Task AboutPlayer(string data)
        {
            await new AboutPlayerActions(_gameData).Handle(data);
        }

        public async Task Notes(string data)
        {
            await new NoteActions(_gameData).Handle(data);
        }

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> GetBaseActions()
        {
            var buttons = new HashSet<InlineKeyboardButton>
            {
                UITools.GetButton<HeroActions>(Constants.LookAround)
            };

            return new List<IEnumerable<InlineKeyboardButton>> { buttons, GetPersonalActions() };
        }

        public static IEnumerable<InlineKeyboardButton> GetPersonalActions()
        {
            var buttons = new HashSet<InlineKeyboardButton>
            {
                UITools.GetButton<HeroActions>(Constants.Quests),
                UITools.GetButton<HeroActions>(Constants.Notes),
                InlineKeyboardButton.WithCallbackData("🎒"),
                UITools.GetButton<HeroActions>(Constants.AboutPlayer)
            };

            return buttons;
        }

        public static class Constants
        {
            public static readonly string LookAround = "LOOKAROUND";
            public static readonly string Quests = "QUESTS";
            public static readonly string Notes = "NOTES";
            public static readonly string AboutPlayer = "ABOUTPLAYER";
        }
    }
}
