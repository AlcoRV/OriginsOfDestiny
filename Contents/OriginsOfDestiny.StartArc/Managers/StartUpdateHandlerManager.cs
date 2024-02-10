using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers;
using OriginsOfDestiny.StartArc.Models.MessageHandlers;

namespace OriginsOfDestiny.StartArc.Managers;

public class StartUpdateHandlerManager : ITelegramUpdateHandlerManager
{
    public ICallbackQueryHandler GetCallbackQueryHandler(string code)
    {
        return new TestCallbackQueryHandler();
    }

    public IMessageHandler GetMessageHandler(string code)
    {
        return new TestMessageHandler();
    }
}
