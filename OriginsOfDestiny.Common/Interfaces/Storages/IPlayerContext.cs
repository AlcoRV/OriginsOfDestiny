using OriginsOfDestiny.Data.Models.Entity;
using OriginsOfDestiny.Data.Enums;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IPlayerContext
{
    public Hero Hero { get; set; }
    public GameArc Arc { get; set; }
}
