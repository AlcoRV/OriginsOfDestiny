using OriginsOfDestiny.Data.Models.Items;
using OriginsOfDestiny.DataObjects.Interfaces.Items;

namespace OriginsOfDestiny.DataObjects.Models.Items.Pickups
{
    public class Note : IItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Volume { get; set; } = 0;
        public int Cost { get; set; } = 0;
        public string Description { get; set; }
        public bool Empty { get; set; } = true;
    }
}
