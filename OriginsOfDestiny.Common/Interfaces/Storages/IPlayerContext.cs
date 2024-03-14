using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Entity;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IPlayerContext
{
    public Hero Hero { get; set; }
    public GameArc Arc { get; set; }
}
