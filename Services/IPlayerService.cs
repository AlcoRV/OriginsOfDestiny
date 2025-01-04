using OriginsOfDestiny.Models.Characters;

namespace OriginsOfDestiny.Services
{
    public interface IPlayerService
    {
        Player GetByTelegramId(long telegramId);
    }
}
