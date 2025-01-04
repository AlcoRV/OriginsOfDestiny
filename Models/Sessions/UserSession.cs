using OriginsOfDestiny.Models.Characters;
using OriginsOfDestiny.Models.Dialogs;

namespace OriginsOfDestiny.Models.Sessions
{
    public class UserSession
    {
        public long Id { get; set; }
        public virtual Player Player { get; set; }

        public string? ActiveDialogId { get; set; }
        public virtual Dialog? ActiveDialog { get; set; }
    }
}
