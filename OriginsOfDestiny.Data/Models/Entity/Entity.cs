using OriginsOfDestiny.Data.Enums;

namespace OriginsOfDestiny.Data.Models.Entity;

public abstract class Entity
{
    public string Name { get; set; }
    public virtual int HP { get; set; }
    public virtual Gender Gender { get; set; }
}
