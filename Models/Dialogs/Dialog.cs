namespace OriginsOfDestiny.Models.Dialogs
{
    public class Dialog
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public Dictionary<string, string> Responses { get; set; } = [];
    }
}
