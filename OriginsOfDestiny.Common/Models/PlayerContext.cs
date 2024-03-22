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

        if(percentageHealth == 100)
        {
            return string.Format(resourceHelper.GetValue(Constants.Health), "😃", percentageHealth);
        }
        else if (percentageHealth > 75)
        {
            return string.Format(resourceHelper.GetValue(Constants.Health), "🙂", percentageHealth);
        }
        else if (percentageHealth > 50)
        {
            return string.Format(resourceHelper.GetValue(Constants.Health), "😐", percentageHealth);
        }
        else if (percentageHealth > 25)
        {
            return string.Format(resourceHelper.GetValue(Constants.Health), "😕", percentageHealth);
        }
        else if (percentageHealth > 10)
        {
            return string.Format(resourceHelper.GetValue(Constants.Health), "😫", percentageHealth);
        }
        else if (percentageHealth > 5)
        {
            return string.Format(resourceHelper.GetValue(Constants.Health), "💀", percentageHealth);
        }
        else
        {
            return resourceHelper.GetValue(Constants.Critical);
        }
    }

    private static class Constants
    {
        public static readonly string Health = "HEALTH";
        public static readonly string Critical = "CRITICAL";
    }
}
