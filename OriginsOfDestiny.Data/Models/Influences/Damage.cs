using OriginsOfDestiny.DataObjects.Enums;

namespace OriginsOfDestiny.DataObjects.Models.Influences
{
    public readonly struct Damage
    {
        public Damage(int min, int max, Element element = Element.None) {
            Element = element;

            Value = new Random().Next(min, max);
        }

        public Element Element { init; get; }
        public int Value { init; get; }
    }
}
