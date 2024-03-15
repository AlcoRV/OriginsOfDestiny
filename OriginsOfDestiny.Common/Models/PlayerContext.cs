using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Entity;
using OriginsOfDestiny.Data.Models.Locations;

namespace OriginsOfDestiny.Common.Models;

public class PlayerContext : IPlayerContext
{
    public Hero Hero { get; set; } = new Hero();
    public GameArc Arc { get; set; }
    public Area Area { get; set; }
}
