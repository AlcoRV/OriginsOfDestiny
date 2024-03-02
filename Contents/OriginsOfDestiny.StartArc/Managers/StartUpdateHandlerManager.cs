using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers;
using OriginsOfDestiny.StartArc.Models.MessageHandlers;

namespace OriginsOfDestiny.StartArc.Managers
{
    using Constants = Constants.Constants;

    public class StartUpdateHandlerManager : ITelegramUpdateHandlerManager
    {
        public ICallbackQueryHandler GetCallbackQueryHandler(string code)
        {
            if (new[] { 
                Constants.Messages.SimonStart.Me.Who,
                Constants.Messages.SimonStart.Me.How,
                Constants.Messages.SimonStart.Me.Where
            }
            .ToList()
            .Contains(code))
            {
                return new SimonStartCallbackQueryHandler();
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
}
