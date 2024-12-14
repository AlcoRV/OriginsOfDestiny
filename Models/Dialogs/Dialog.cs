using OriginsOfDestiny.Models.Sessions;

namespace OriginsOfDestiny.Models.Dialogs
{
    public class Dialog
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public Dictionary<string, string>? Responses { get; set; } = null;

        public bool NeedReplace { get; set; } = true;

        public virtual List<UserSession> Sessions { get; set; }
    }
}
