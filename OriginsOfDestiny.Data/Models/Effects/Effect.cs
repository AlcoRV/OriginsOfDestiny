using OriginsOfDestiny.Data.Enums;

namespace OriginsOfDestiny.Data.Models.Effects;

public class Effect
{
    public Dictionary<Element, double> ElementEffects = new();

    public int Health {  get; set; }
}

 