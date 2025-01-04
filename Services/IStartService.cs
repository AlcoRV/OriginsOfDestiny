using OriginsOfDestiny.Telegram;

namespace OriginsOfDestiny.Services
{
    public interface IStartService : ICallbackHandler, IMessageHandler
    {
        Task Start(long id, CancellationToken cancellationToken);
    }
}
