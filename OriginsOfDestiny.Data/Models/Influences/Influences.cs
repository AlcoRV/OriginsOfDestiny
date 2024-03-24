using OriginsOfDestiny.DataObjects.Enums;

namespace OriginsOfDestiny.DataObjects.Models.Influences;

public class Influences
{
    public Dictionary<Element, double> Effects = new() {
        { Element.None, 1 },
        { Element.Fire, 1 },
        { Element.Water, 1 },
        { Element.Earth, 1 },
        { Element.Wind, 1 }
    };
}

 