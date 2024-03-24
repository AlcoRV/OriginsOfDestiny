using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Influences;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;
using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.Data.Models.Items.InteractiveItems;

public class Hollow : IInteractiveItem, IDamageTo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public double Probability { get; set; } = 0.5;
    public IEnumerable<IItem> Loot { get; set; }

    public Damage Damage => new()
    {
        Element = Element.Earth,
        Min = 15,
        Max = 30
    };

    public bool DamageTo(IMortal mortal)
    {
        return mortal.GetDamage(Damage);
    }

    public static class Messages
    {
        public static readonly string DamageTo = "DAMAGETO";
    }
}
