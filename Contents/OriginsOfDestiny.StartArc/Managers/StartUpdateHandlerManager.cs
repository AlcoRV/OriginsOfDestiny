﻿using OriginsOfDestiny.Common.Interfaces.Handlers;
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
            var handleCode = code.Split("_")[0];

            if (handleCode.Equals(Constants.Messages.SimonStart.Name))
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
