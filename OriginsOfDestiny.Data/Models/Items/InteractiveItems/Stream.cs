﻿using OriginsOfDestiny.Data.Models.Effects;
using OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems;

namespace OriginsOfDestiny.Data.Models.Items.InteractiveItems;

public class Stream : IInteractiveItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public Effect PositiveEffect { get; set; } = new Effect() { Health = 20 };
    public Effect NegativeEffect { get; set; } = new Effect() { Health = -20 };
    public double Probability { get; set; } = 0.5;
}
