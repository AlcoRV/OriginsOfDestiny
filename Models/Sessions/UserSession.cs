namespace OriginsOfDestiny.Models.Sessions
{
    public class UserSession
    {
        public long Id { get; set; }

        public Guid ActiveDialogId { get; set; }
    }
}
