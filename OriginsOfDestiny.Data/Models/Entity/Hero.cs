using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Data.Models.Items;

namespace OriginsOfDestiny.Data.Models.Entity;

public class Hero : Entity
{
    public override Gender Gender { get; set; } = Gender.Woman;
    public override int HP { get; set; } = 100;
    public override string Name { get; set; }
    public override IEnumerable<Item> Inventory { get; set; } = new HashSet<Item>();
}
