using OriginsOfDestiny.Models.Sessions;

namespace OriginsOfDestiny.Models.Characters
{
    public class Player : Character
    {
        public int Expirience { get; set; }

        public bool IsActive { get; set; }
        public DateTime? DeactivationDate { get; set; }

        public long TelegramId { get; set; }
        public virtual UserSession Session { get; set; }
    }
}
