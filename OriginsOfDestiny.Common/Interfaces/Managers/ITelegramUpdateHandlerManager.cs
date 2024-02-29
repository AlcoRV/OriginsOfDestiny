using OriginsOfDestiny.Common.Interfaces.Handlers;

namespace OriginsOfDestiny.Common.Interfaces.Managers;

public interface ITelegramUpdateHandlerManager
{
    public IMessageHandler GetMessageHandler(string code);
    public ICallbackQueryHandler GetCallbackQueryHandler(string code);
}
