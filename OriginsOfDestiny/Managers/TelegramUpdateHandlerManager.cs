using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Test;

namespace OriginsOfDestiny.Managers;

public class TelegramUpdateHandlerManager : ITelegramUpdateHandlerManager
{
    public ICallbackQueryHandler GetCallbackQueryHandler(string callbackMessage)
    {
        return new TestCallbackQueryHandle();
    }

    public IMessageHandler GetMessageHandler(string message)
    {
        return new TestMessageHandler();
    }
}
