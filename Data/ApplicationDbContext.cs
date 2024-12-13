using Microsoft.EntityFrameworkCore;
using OriginsOfDestiny.Models.Dialogs;
using OriginsOfDestiny.Models.Sessions;

namespace OriginsOfDestiny.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            
        }

        public DbSet<UserSession> Sessions { get; set; }

        public DbSet<Dialog> Dialogs { get; set; }
    }
}
