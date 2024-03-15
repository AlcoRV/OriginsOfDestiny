using OriginsOfDestiny.Data.Models.Effects;

namespace OriginsOfDestiny.Data.Models.Items.InteractiveItems;

public class Stream : InteractiveItem
{
    public override string Name { get; set; }

    public Effect PositiveEffect { get; set; } = new Effect() { Health = 20 };
    public Effect NegativeEffect { get; set; } = new Effect() { Health = -20 };
    public double Probability { get; set; } = 0.5;
}
