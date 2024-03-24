using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Influences;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;
using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.Data.Models.Items.InteractiveItems;

public class Stream : IInteractiveItem, IDamageTo, IHealTo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public double Probability { get; set; } = 0.5;
    public Damage Damage { get; } = new()
    {
        Element = Element.Water,
        Min = 15,
        Max = 25
    };
    public Heal Heal { get; } = new Heal()
    {
        Element = Element.Water,
        Min = 15,
        Max = 200
    };

    public bool DamageTo(IMortal mortal)
    {
        return mortal.GetDamage(Damage);
    }

    public void HealTo(IMortal mortal)
    {
        mortal.GetHealing(Heal);
    }

    public static class Messages
    {
        public static readonly string DamageTo = "DAMAGETO";
        public static readonly string HealTo = "HEALING";
    }
}
