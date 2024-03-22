using OriginsOfDestiny.Data.Models.Effects;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;
using OriginsOfDestiny.DataObjects.Interfaces.Items;

namespace OriginsOfDestiny.Data.Models.Items.InteractiveItems;

public class Hollow : IInteractiveItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public Effect NegativeEffect { get; set; } = new Effect() { Health = -20 };
    public double Probability { get; set; } = 0.5;
    public IEnumerable<IItem> Loot { get; set; }
}
