using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using Stream = OriginsOfDestiny.Data.Models.Items.InteractiveItems.Stream;
using DConstants = OriginsOfDestiny.Data.Constants.DConstants;
using OriginsOfDestiny.Game.Constants;
using OriginsOfDestiny.Common.UI;
using OriginsOfDestiny.DataObjects.Enums;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class HeroActions
    {
        private readonly IGameData _gameData;
        private static readonly ResourceHelper<HeroActions> ResourceHelper = new();

        public HeroActions(IGameData gameData)
        {
            _gameData = gameData;
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
                    textButtons.Add(
                        InlineKeyboardButton.WithCallbackData(
                            new ResourceHelper<Stream>()
                            .GetValue(DConstants.Messages.Items.Use),
                            $"{DConstants.Messages.Items.Use}_{nameof(Stream)}_{stream.Id}"
                            )
                        );
                }
                else if (iItem is Hollow duplo)
                {
                    textButtons.Add(
                        InlineKeyboardButton.WithCallbackData(
                            new ResourceHelper<Hollow>()
                            .GetValue(DConstants.Messages.Items.Use),
                            $"{DConstants.Messages.Items.Use}_{nameof(Hollow)}_{duplo.Id}"
                            )
                        );
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

        public async Task AboutPlayer()
        {
            var hero = _gameData.ClientData.PlayerContext.Hero;
            var genderPostfix = hero.Gender == Gender.Man ? "_M" : "_W";

            var sb = new StringBuilder();

            sb.AppendLine("🔺 " +
                string.Format(
                    ResourceHelper.GetValue(Constants.Hero.Name),
                    hero.Name
                    ) 
                );

            sb.AppendLine("🔺 " +
                    ResourceHelper.GetValue(Constants.Hero.Gender + genderPostfix)
                );

            sb.AppendLine("🔺 " + ResourceHelper.GetValue(Constants.Hero.Personality));
            sb.AppendLine("🔺 " + ResourceHelper.GetValue(Constants.Hero.Married + genderPostfix));

            var element = hero.Element == Element.None
                ? Constants.Elements.Element
                : hero.Element.ToString().ToUpper();
            sb.AppendLine(ResourceHelper.GetValue(element));

            await _gameData.ClientData.EditMainMessageAsync(
             caption: sb.ToString(),
             replyMarkup: new InlineKeyboardMarkup(GetBaseActions())
            );
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
                InlineKeyboardButton.WithCallbackData("📝"),
                InlineKeyboardButton.WithCallbackData("🎒"),
                UITools.GetButton<HeroActions>(Constants.AboutPlayer)
            };

            return buttons;
        }

        public static class Constants
        {
            public static readonly string LookAround = "LOOKAROUND";
            public static readonly string Quests = "QUESTS";
            public static readonly string AboutPlayer = "ABOUTPLAYER";

            public static class Hero
            {
                public static readonly string Name = "HERONAME";
                public static readonly string Gender = "HEROGENDER";
                public static readonly string Personality = "PERSONALITY";
                public static readonly string Married = "MARRIED";
            }

            public static class Elements
            {
                public static readonly string Fire = "FIRE";
                public static readonly string Water = "WATER";
                public static readonly string Wind = "WIND";
                public static readonly string Earth = "EARTH";
                public static readonly string Element = "ELEMENT";
            }
        }
    }
}
