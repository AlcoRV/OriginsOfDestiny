namespace OriginsOfDestiny.DataObjects.Interfaces.InteractiveItems
{
    public interface IInteractiveItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static class Messages
        {
            public static readonly string Interact = "INTERACT";
        }
    }
}
