using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Models;

namespace OriginsOfDestiny.DataObjects.Models.Entity.Spirits;

public class HighElementSpirit : IOpponent
{
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public Attitude Attitude { get; set; }

    public string Picture { get; set; }
}
