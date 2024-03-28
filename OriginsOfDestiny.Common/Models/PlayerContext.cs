using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Locations;
using OriginsOfDestiny.DataObjects.Interfaces.Models;
using OriginsOfDestiny.DataObjects.Models.Entity;

namespace OriginsOfDestiny.Common.Models;

public class PlayerContext : IPlayerContext
{
    public Hero Hero { get; set; } = new Hero();
    public GameArc Arc { get; set; }
    public Area Area { get; set; }
    public IOpponent Opponent { get; set; }

    public string GetHeroHealth()
    {
        var resourceHelper = new ResourceHelper<PlayerContext>();
        int percentageHealth = (int)((Hero.HP * 1.0) / Hero.MaxHP * 100);
        string emoji;

        switch (percentageHealth)
        {
            case int n when n == 100:
                emoji = "😃";
                break;
            case int n when n > 75:
                emoji = "🙂";
                break;
            case int n when n > 50:
                emoji = "😐";
                break;
            case int n when n > 25:
                emoji = "😕";
                break;
            case int n when n > 10:
                emoji = "😫";
                break;
            case int n when n > 5:
                emoji = "💀";
                break;
            case int n when n == 0:
                emoji = "🪦";
                break;
            default:
                return string.Format(resourceHelper.GetValue(Constants.Critical), Hero.HP);
        }

        return string.Format(resourceHelper.GetValue(Hero.HP > 0 ? Constants.Health : Messages.Dead + new Random().Next(1, 6)), emoji, percentageHealth, Hero.HP);
    }

    private static class Messages
    {
        public static string Dead => "DEAD";
    }

    private static class Constants
    {
        public static readonly string Health = "HEALTH";
        public static readonly string Critical = "CRITICAL";
    }
}
