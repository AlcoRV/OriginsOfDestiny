namespace OriginsOfDestiny.Data.Models.Items
{
    public abstract class Item
    {
        public abstract Guid Id { get; set; }
        public abstract string Name { get; set; }
        public abstract int Volume { get; set; }
        public abstract int Cost { get; set; }
    }
}
