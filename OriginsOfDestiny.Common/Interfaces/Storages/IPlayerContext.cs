using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Locations;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.DataObjects.Interfaces.Models;
using OriginsOfDestiny.DataObjects.Models.Entity;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IPlayerContext
{
    public Hero Hero { get; set; }
    public GameArc Arc { get; set; }
    public Area Area { get; set; }
    public IOpponent Opponent { get; set; }
    public IItem ActiveItem { get; set; }

    public string GetHeroHealth();
}
