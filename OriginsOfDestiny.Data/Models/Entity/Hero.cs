using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.DataObjects.Interfaces.Models;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;

namespace OriginsOfDestiny.DataObjects.Models.Entity;

public class Hero : IEntity, IMortal, IHasInventory
{
    public Gender Gender { get; set; } = Gender.Woman;
    public int MaxHP { get; set; } = 100;
    public int HP { get; set; } = 100;
    public string Name { get; set; }
    public IEnumerable<IItem> Inventory { get; set; } = new HashSet<IItem>();

}
