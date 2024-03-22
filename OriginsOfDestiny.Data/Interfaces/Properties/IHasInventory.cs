using OriginsOfDestiny.DataObjects.Interfaces.Items;

namespace OriginsOfDestiny.DataObjects.Interfaces.Properties
{
    public interface IHasInventory
    {
        public IEnumerable<IItem> Inventory { get; set; }
    }
}
