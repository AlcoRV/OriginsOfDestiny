using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Game.Enums;
using OriginsOfDestiny.Game.Models.Entity;

namespace OriginsOfDestiny.Common.Models;

public class PlayerContext : IPlayerContext
{
    public MainHero MainHero { get; set; }
    public GameArc Arc { get; set; }
}
