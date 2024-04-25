using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.Common.UI;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;
using OriginsOfDestiny.DataObjects.Models.Entity;
using OriginsOfDestiny.DataObjects.Models.Items.Pickups;
using OriginsOfDestiny.Game.Models.WaitingFor;
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
            var buttons = new HashSet<IEnumerable<InlineKeyboardButton>>();

            foreach (var note in notes)
            {
                buttons.Add(new[] { InlineKeyboardButton.WithCallbackData(note.Name, $"{Constants.Show}:{note.Id}") });
            }

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: ResourceHelper.GetValue(Constants.BaseMessage),
                 replyMarkup: new InlineKeyboardMarkup(buttons.Union(HeroActions.GetBaseActions()))
                );
        }

        public async Task Handle(string data)
        {
            if(data.Equals(Constants.Name, StringComparison.OrdinalIgnoreCase))
            {
                await ShowBaseActions();
            }
            else
            {
                var parameters = data.Split(':');
                if(parameters.Length < 2 ) { return; }

                var note = _hero.Inventory.FirstOrDefault(it => it.Id == Guid.Parse(parameters[1])) as Note;
                if (note != null)
                {
                    if (parameters[0].Equals(Constants.Show)) { await ShowItem(note); }
                    else if (parameters[0].Equals(Constants.WriteNew)) { await WriteItem(note); }
                    else if (parameters[0].Equals(Constants.Leave)) { await SelectPlace(note); }
                }

                var place = _gameData.ClientData.PlayerContext.Area.InteractiveItems.FirstOrDefault(it => it.Id == Guid.Parse(parameters[1])) as ILoot;
                if (place != null)
                {
                    if (parameters[0].Equals(Constants.Leave)) { await SelectPlace(place); }
                }
            }

        }

        private async Task SelectPlace(Note note)
        {
            var enviroment = _gameData.ClientData.PlayerContext.Area.InteractiveItems.Where(ii => ii is ILoot);

            if (!enviroment.Any())
            {
                await _gameData.ClientData.EditMainMessageAsync(
                 caption: ResourceHelper.GetValue(Constants.CantLeave),
                 replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                );
                return;
            }

            _gameData.ClientData.PlayerContext.ActiveItem = note;
            var buttons = enviroment.Select(ii => new[] { InlineKeyboardButton.WithCallbackData(ii.Name, $"{Constants.Leave}:{ii.Id}") });

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: ResourceHelper.GetValue(Constants.FoundPlaces),
                 replyMarkup: new InlineKeyboardMarkup(buttons.Union(HeroActions.GetBaseActions()))
                );
            return;
        }

        private async Task SelectPlace(ILoot place)
        {
            var note = _gameData.ClientData.PlayerContext.ActiveItem;
            _gameData.ClientData.PlayerContext.ActiveItem = null;

            (place.Loot as HashSet<IItem>).Add(note);
            (_hero.Inventory as HashSet<IItem>).Remove(note);

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: ResourceHelper.GetValue(Constants.NewSecret),
                 replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                );
            return;
        }

        private async Task WriteItem(Note note)
        {
            _gameData.ClientData.WaitingForMessage = WaitingForBaseMessageHandler.Factory.Create<WaitingForNewNote>(_gameData);
            _gameData.ClientData.PlayerContext.ActiveItem = note;

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: ResourceHelper.GetValue(Constants.WriteNewEnter),
                 replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
                );
        }

        private async Task ShowItem(Note note)
        {
            var buttons = new HashSet<InlineKeyboardButton>();

            string? caption;
            if (note.Empty)
            {
                caption = ResourceHelper.GetValue(Constants.ShowEmpty);
                buttons.Add(UITools.GetButton<NoteActions>(Constants.WriteNew, ":", note.Id.ToString()));
            }
            else
            {
                caption = string.Format(ResourceHelper.GetValue(Constants.Show), note.Description);
                buttons.Add(UITools.GetButton<NoteActions>(Constants.Leave, ":", note.Id.ToString()));
            }

            await _gameData.ClientData.EditMainMessageAsync(
                 caption: caption,
                 replyMarkup: new InlineKeyboardMarkup(new[] { buttons }.Union(HeroActions.GetBaseActions()))
                );
        }

        public static class Constants
        {
            public static readonly string Name = "NOTES";

            public static readonly string Show = "NOTES_SHOW";
            public static readonly string ShowEmpty = "NOTES_SHOW_EMPTY";
            public static readonly string WriteNew = "NOTES_WRITENEW";
            public static readonly string WriteNewEnter = "NOTES_WRITENEW_ENTER";
            public static readonly string Leave = "NOTES_LEAVE";
            public static readonly string FoundPlaces = "NOTES_LEAVE_PLACES";
            public static readonly string NewSecret = "NOTES_LEAVE_NEW_SECRET";
            public static readonly string CantLeave = "NOTES_CANTLEAVE";

            public static readonly string NoNotes = "NONOTES";
            public static readonly string BaseMessage = "BASEMESSAGE";
        }
    }
}
