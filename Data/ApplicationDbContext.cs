using Microsoft.EntityFrameworkCore;
using OriginsOfDestiny.Models.Characters;
using OriginsOfDestiny.Models.Dialogs;
using OriginsOfDestiny.Models.Sessions;

namespace OriginsOfDestiny.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .ToTable(nameof(Character));

            modelBuilder.Entity<Player>()
                .ToTable(nameof(Player));

            modelBuilder.Entity<Player>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<UserSession>()
                .HasKey(us => us.Id);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Session)
                .WithMany(us => us.Players)
                .HasForeignKey(p => p.TelegramId)
                .HasPrincipalKey(us => us.Id);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserSession> Sessions { get; set; }

        public DbSet<Dialog> Dialogs { get; set; }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
