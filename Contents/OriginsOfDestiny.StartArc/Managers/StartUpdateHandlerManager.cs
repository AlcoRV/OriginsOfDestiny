using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers;
using OriginsOfDestiny.StartArc.Models.MessageHandlers;

namespace OriginsOfDestiny.StartArc.Managers;

public class StartUpdateHandlerManager : ITelegramUpdateHandlerManager
{
    public ICallbackQueryHandler GetCallbackQueryHandler(string code)
    {
        if (new[] { "who", "how", "where" }.ToList().Contains(code))
        {
            return new SimonStartQueryHandler();
        }
        return new TestCallbackQueryHandler();
    }

    public IMessageHandler GetMessageHandler(string code)
    {
        return code switch
        {
            "/start" => new StartMessageHandler(),
            _ => new TestMessageHandler()
        };
    }
}
