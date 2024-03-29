using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.UI;
using OriginsOfDestiny.DataObjects.Models.Entity;
using OriginsOfDestiny.DataObjects.Models.Items.Pickups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class NoteActions
    {
        private readonly IGameData _gameData;
        private readonly Hero _hero;
        private static readonly ResourceHelper<NoteActions> ResourceHelper = new();

        public NoteActions(IGameData gameData)
        {
            _gameData = gameData;
            _hero = gameData.ClientData.PlayerContext.Hero;
        }

        public async Task ShowBaseActions()
        {
            var notes = _hero.Inventory
                .Where(it => it is Note note)
                .Select(it => it as Note);

            if (!notes.Any())
            {
                await _gameData.ClientData.EditMainMessageAsync(
                 caption: ResourceHelper.GetValue(Constants.NoNotes),
                 replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                );
                return;
            }
            var buttons = new HashSet<InlineKeyboardButton>();

            if (notes.Any(n => !n.Empty))
            {
                buttons.Add(UITools.GetButton<NoteActions>(Constants.Show, ":0"));
                buttons.Add(UITools.GetButton<NoteActions>(Constants.Leave));
            }

            if (notes.Any(n => n.Empty))
            {
                buttons.Add(UITools.GetButton<NoteActions>(Constants.WriteNew));
            }

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: ResourceHelper.GetValue(Constants.BaseMessage),
                 replyMarkup: new InlineKeyboardMarkup(new HashSet<IEnumerable<InlineKeyboardButton>> { buttons }.Union(HeroActions.GetBaseActions()))
                );
        }

        public static class Constants
        {
            public static readonly string Show = "NOTES_SHOW";
            public static readonly string WriteNew = "NOTES_WRITENEW";
            public static readonly string Leave = "NOTES_LEAVE";

            public static readonly string NoNotes = "NONOTES";
            public static readonly string BaseMessage = "BASEMESSAGE";
        }
    }
}
