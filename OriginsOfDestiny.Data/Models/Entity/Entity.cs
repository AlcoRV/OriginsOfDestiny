using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Items;

namespace OriginsOfDestiny.Data.Models.Entity;

public abstract class Entity
{
    public abstract string Name { get; set; }
    public abstract int HP { get; set; }
    public abstract Gender Gender { get; set; }
    public abstract IEnumerable<Item> Inventory { get; set; }
}
