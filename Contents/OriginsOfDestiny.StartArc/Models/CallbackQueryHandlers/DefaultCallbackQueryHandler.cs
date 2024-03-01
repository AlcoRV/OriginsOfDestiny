using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.CallbackQueryHandlers;

public class DefaultCallbackQueryHandler : ICallbackQueryHandler
{
    public Task Handle(IGameData gameData, CallbackQuery callbackQuery)
    {
        throw new NotImplementedException();
    }
}
