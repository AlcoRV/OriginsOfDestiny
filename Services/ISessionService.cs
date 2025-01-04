using OriginsOfDestiny.Models.Sessions;

namespace OriginsOfDestiny.Services
{
    public interface ISessionService
    {
        public Task<UserSession> GetOrCreate(long userId);

        public Task Udpate(UserSession session);

        public Task Remove(long userId);
    }
}
