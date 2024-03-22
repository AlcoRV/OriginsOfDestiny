using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using Stream = OriginsOfDestiny.Data.Models.Items.InteractiveItems.Stream;
using Telegram.Bot;
using DConstants = OriginsOfDestiny.Data.Constants.DConstants;
using OriginsOfDestiny.Game.Constants;

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
                sb.AppendLine("\t" + iItem.Name);
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

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: textMessage,
                 replyMarkup: new InlineKeyboardMarkup(textButtons.Chunk(1))
                );
        }

        public static IEnumerable<InlineKeyboardButton> GetBaseActions()
        {
            var buttons = new HashSet<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData(
                    ResourceHelper.GetValue(GConstants.Messages.HeroActions.LookAround),
                    GConstants.Messages.HeroActions.LookAround)
            };

            return buttons;
        }
    }
}
