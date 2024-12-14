using OriginsOfDestiny.Models.Characters;

namespace OriginsOfDestiny.Models.Sessions
{
    public class UserSession
    {
        public long Id { get; set; }

        public Guid ActiveDialogId { get; set; }

        public virtual Player Player { get; set; }
    }
}
