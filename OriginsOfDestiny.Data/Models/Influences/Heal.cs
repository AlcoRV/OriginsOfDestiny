using OriginsOfDestiny.DataObjects.Enums;

namespace OriginsOfDestiny.DataObjects.Models.Influences
{
    public struct Heal
    {
        public Element Element { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public readonly int Value => new Random().Next(Min, Max);
    }
}
