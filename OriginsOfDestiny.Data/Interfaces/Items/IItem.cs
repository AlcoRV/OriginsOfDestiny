namespace OriginsOfDestiny.DataObjects.Interfaces.Items
{
    public interface IItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Volume { get; set; }
        public int Cost { get; set; }
    }
}
