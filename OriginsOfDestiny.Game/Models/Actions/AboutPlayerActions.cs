using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.UI;
using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Models.Entity;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Game.Models.Actions
{
    public class AboutPlayerActions
    {
        private readonly IGameData _gameData;
        private readonly Hero _hero;
        private static readonly ResourceHelper<AboutPlayerActions> ResourceHelper = new();

        public AboutPlayerActions(IGameData gameData)
        {
            _gameData = gameData;
            _hero = gameData.ClientData.PlayerContext.Hero;
        }

        public async Task Handle(string data)
        {
            if (data.Equals(Constants.AboutPlayer, StringComparison.OrdinalIgnoreCase))
            {
                await GetBaseMenu();
            }
            else if(data.Equals(Constants.Hero.Influence.Influences, StringComparison.OrdinalIgnoreCase))
            {
                await GetInfluences();
            }
        }

        private async Task GetInfluences()
        {
            var sb = new StringBuilder();
            sb.Append(ResourceHelper.GetValue(Constants.Hero.MainElement) + '\t');

            var element = _hero.Element == Element.None
                ? Constants.Elements.Element
                : _hero.Element.ToString().ToUpper();
            sb.AppendLine(ResourceHelper.GetValue(element));
            sb.AppendLine();

            var elementsInfluence = _hero.Influences.Effects.Where(e => e.Value != 1);
            if (elementsInfluence.Any())
            {
                foreach (var influence in elementsInfluence)
                {
                    var type = influence.Value > 1 ? Constants.Hero.Influence.Resistance : Constants.Hero.Influence.Weakness;
                    var value = (int)Math.Abs((influence.Value - 1) * 100);

                    sb.AppendLine($"🌀 {ResourceHelper.GetValue(type)} {ResourceHelper.GetValue("INF" + influence.Key.ToString().ToUpper())}: {value}%");
                }
            }
            else
            {
                sb.AppendLine("🌀 " + ResourceHelper.GetValue(Constants.Hero.Influence.Without));
            }


            await _gameData.ClientData.EditMainMessageAsync(
             caption: sb.ToString(),
             replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
            );
        }

        private async Task GetBaseMenu()
        {
            var hero = _gameData.ClientData.PlayerContext.Hero;
            var genderPostfix = hero.Gender == Gender.Man ? "_M" : "_W";

            var sb = new StringBuilder();

            sb.AppendLine(ResourceHelper.GetValue(Constants.AboutPlayer));
            sb.AppendLine();

            sb.AppendLine("🔺" +
                string.Format(
                    ResourceHelper.GetValue(Constants.Hero.Name),
                    hero.Name
                    ));

            sb.AppendLine("🔺 " +
                    ResourceHelper.GetValue(Constants.Hero.Gender + genderPostfix)
                );

            sb.AppendLine("🔺 " + ResourceHelper.GetValue(Constants.Hero.Personality));
            sb.AppendLine("🔺 " + ResourceHelper.GetValue(Constants.Hero.Married + genderPostfix));

            var buttons = new HashSet<InlineKeyboardButton>() { UITools.GetButton<AboutPlayerActions>(Constants.Hero.Influence.Influences) };

            await _gameData.ClientData.EditMainMessageAsync(
             caption: sb.ToString(),
             replyMarkup: new InlineKeyboardMarkup(new[] { buttons }.Union(HeroActions.GetBaseActions()))
            );
        }

        public static class Constants
        {
            public static readonly string AboutPlayer = "ABOUTPLAYER";

            public static class Hero
            {
                public static readonly string Name = "HERONAME";
                public static readonly string MainElement = "MAINELEMENT";
                public static readonly string Gender = "HEROGENDER";
                public static readonly string Personality = "PERSONALITY";
                public static readonly string Married = "MARRIED";
                public static class Influence
                {
                    public static readonly string Influences = "ABOUTPLAYER_INFLUENCES";
                    public static readonly string Without = "WITHOUTINFLUENCE";
                    public static readonly string Weakness = "WEAKNESS";
                    public static readonly string Resistance = "RESISTANCE";

                    public static readonly string Fire = "INFFIRE";
                    public static readonly string Water = "INFWATER";
                    public static readonly string Wind = "INFWIND";
                    public static readonly string Earth = "INFEARTH";
                }
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
