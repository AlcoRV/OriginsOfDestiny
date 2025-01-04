using Microsoft.EntityFrameworkCore;
using OriginsOfDestiny.Data;
using OriginsOfDestiny.Models.Characters;
using OriginsOfDestiny.Models.Sessions;

namespace OriginsOfDestiny.Services
{
    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<UserSession> GetOrCreate(long userId)
        {
            var session = await _dbContext.Sessions
                .Where(s => s.Id == userId)
                .Include(s => s.ActiveDialog)
                .Include(s => s.Player)
                .FirstOrDefaultAsync();

            if(session != null) { return session; }

            session = new UserSession()
            {
                Id = userId,
                ActiveDialogId = @"\start"
            };

            await _dbContext.Sessions.AddAsync(session);

            var player = CreateDefaultPlayer(userId);

            await _dbContext.Players.AddAsync(player);

            await _dbContext.SaveChangesAsync();

            return session;
        }

        public async Task Remove(long userId)
        {
            var session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == userId);

            if (session != null)
            {
                _dbContext.Sessions.Remove(session);

                var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.TelegramId == userId);
                var character = await _dbContext.Characters.FirstOrDefaultAsync(ch => ch.Id == player.Id);
                _dbContext.Players.Remove(player);
                _dbContext.Characters.Remove(character);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Udpate(UserSession session)
        {
            _dbContext.Sessions.Update(session);
            await _dbContext.SaveChangesAsync();
        }

        public static Player CreateDefaultPlayer(long userId)
        {
            return new Player
            {
                Id = Guid.NewGuid(),
                Health = 100,
                MaxHealth = 100,
                Level = 1,
                Expirience = 0,
                Mana = 0,
                Name = "anonimus",
                TelegramId = userId,
                Attributes = new Dictionary<string, int>
                {
                    { "straight", 2 },
                    { "agility", 2 },
                    { "inteligence", 2 },
                    { "luck", 2 }
                }
            };
        }
    }
}
