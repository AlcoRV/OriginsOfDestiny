using OriginsOfDestiny.DataObjects.Interfaces.Items;

namespace OriginsOfDestiny.DataObjects.Interfaces.Properties
{
    public interface ILoot
    {
        public IEnumerable<IItem> Loot { get; set; }
    }
}
