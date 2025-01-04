using OriginsOfDestiny.Telegram;

namespace OriginsOfDestiny.Handlers
{
    public interface IStartService : ICallbackHandler, IMessageHandler {
        Task Start(long id, CancellationToken cancellationToken);
    }
}
