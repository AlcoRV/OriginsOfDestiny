using OriginsOfDestiny.DataObjects.Enums;
using OriginsOfDestiny.DataObjects.Interfaces.Influences;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Properties;
using OriginsOfDestiny.DataObjects.Models.Influences;

namespace OriginsOfDestiny.DataObjects.Models.Items.InteractiveItems;

public class Stream : IInteractiveItem, IDamageTo, IHealTo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public double Probability { get; set; } = 0.5;
    public Damage Damage { get; } = new(15, 25, Element.Water);
    public Heal Heal { get; } = new Heal(15, 200, Element.Water);

    public bool DamageTo(IMortal mortal)
    {
        return mortal.GetDamage(Damage);
    }

    public bool HealTo(IMortal mortal)
    {
        return mortal.GetHealing(Heal);
    }

    public static class Messages
    {
        public static readonly string Kill = "KILL";
        public static readonly string DamageTo = "DAMAGETO";
        public static readonly string HealTo = "HEALING";
    }
}
