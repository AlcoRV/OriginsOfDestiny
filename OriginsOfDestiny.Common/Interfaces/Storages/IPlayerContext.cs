using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Entity;
using OriginsOfDestiny.Data.Models.Locations;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IPlayerContext
{
    public Hero Hero { get; set; }
    public GameArc Arc { get; set; }
    public Area Area { get; set; }
}
