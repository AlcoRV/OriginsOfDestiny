using OriginsOfDestiny.Data;
using OriginsOfDestiny.Models.Characters;
using OriginsOfDestiny.Models.Sessions;
using System.Linq.Expressions;

namespace OriginsOfDestiny.Repositories
{
    public class SessionRepository : IRepository<UserSession>
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context) {
            _context = context;
        }

        public void Create(UserSession entity)
        {
            entity.ActiveDialogId = @"\start";

            _context.Sessions.Add(entity);

            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Health = 100,
                Level = 1,
                Expirience = 0,
                Mana = 0,
                Name = "anonimus",
                TelegramId = entity.Id
            };
            player.Attributes = new() {
                { "straight", 2 },
                    { "agility", 2 },
                    { "inteligence", 2 },
                    { "luck", 2 },
                };

            _context.Players.Add(player);

            _context.SaveChanges();
        }

        public void Delete(UserSession entity)
        {
            _context.Sessions.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<UserSession> Get(Expression<Func<UserSession, bool>> expression)
        {
            return expression == null
                ? _context.Sessions
                : _context.Sessions.Where(expression);
        }

        public void Update(UserSession entity)
        {
            _context.Sessions.Update(entity);
            _context.SaveChanges();
        }
    }
}
