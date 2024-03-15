using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Models.Items.InteractiveItems;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using Stream = OriginsOfDestiny.Data.Models.Items.InteractiveItems.Stream;
using Telegram.Bot;
using GConstants = OriginsOfDestiny.Game.Constants.Constants;
using DConstants = OriginsOfDestiny.Data.Constants.Constants;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class HeroActions
    {
        private readonly IGameData _gameData;

        public HeroActions(IGameData gameData)
        {
            _gameData = gameData;
        }

        public async Task LookAround()
        {
            var area = _gameData.ClientData.PlayerContext.Area;
            var mainMessageId = _gameData.ClientData.MainMessageId;

            var sb = new StringBuilder();
            sb.AppendLine(area.Name);
            sb.AppendLine(area.Description);
            sb.AppendLine("Вы видите:");
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
                            .GetValue(DConstants.Messages.Items.Use)
                            )
                        );
                }
                else if (iItem is Duplo duplo)
                {
                    textButtons.Add(new ResourceHelper<Duplo>().GetValue(DConstants.Messages.Items.Use));
                }
            }

            await _gameData.ClientData.BotClient.EditMessageCaptionAsync(_gameData.ClientData.Id,
                 _gameData.ClientData.MainMessageId,
                 textMessage,
                 replyMarkup: new InlineKeyboardMarkup(textButtons.Chunk(1))
                );
        }

        public static async Task<IEnumerable<InlineKeyboardButton>> GetBaseActions()
        {
            var resourceHelper = new ResourceHelper<HeroActions>();

            var buttons = new HashSet<InlineKeyboardButton>();
            buttons.Add(
                InlineKeyboardButton.WithCallbackData(
                    resourceHelper.GetValue(GConstants.HeroActions.LookAround),
                    GConstants.HeroActions.LookAround)
                );

            return buttons;
        }
    }
}
