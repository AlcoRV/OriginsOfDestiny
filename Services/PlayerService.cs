using OriginsOfDestiny.Data;
using OriginsOfDestiny.Models.Characters;

namespace OriginsOfDestiny.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Player GetByTelegramId(long telegramId)
        {
            return _context.Players.FirstOrDefault(p => p.TelegramId == telegramId);
        }
    }
}
