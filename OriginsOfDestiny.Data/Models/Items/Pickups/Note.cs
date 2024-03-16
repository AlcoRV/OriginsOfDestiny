using OriginsOfDestiny.Data.Models.Items;

namespace OriginsOfDestiny.DataObjects.Models.Items.Pickups
{
    public class Note : Item
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public override string Name { get; set; }
        public override int Volume { get; set; } = 0;
        public override int Cost { get; set; } = 0;
        public string Description { get; set; }
    }
}
