using OriginsOfDestiny.Game.Enums;
using OriginsOfDestiny.Game.Models.Entity;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IPlayerContext
{
    public MainHero MainHero { get; set; }
    public GameArc Arc { get; set; }
}
