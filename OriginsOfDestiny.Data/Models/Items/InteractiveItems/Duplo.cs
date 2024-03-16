using OriginsOfDestiny.Data.Models.Effects;

namespace OriginsOfDestiny.Data.Models.Items.InteractiveItems;

public class Duplo : InteractiveItem
{
    public override Guid Id { get; set; } = Guid.NewGuid();
    public override string Name { get; set; }
    public Effect NegativeEffect { get; set; } = new Effect() { Health = -20 };
    public double Probability { get; set; } = 0.5;
    public IEnumerable<Item> Loot { get; set; }
}
