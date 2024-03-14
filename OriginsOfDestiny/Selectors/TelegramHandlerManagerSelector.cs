using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Data.Enums;
using OriginsOfDestiny.Interfaces;
using OriginsOfDestiny.StartArc.Managers;

namespace OriginsOfDestiny.Selectors;

public class TelegramHandlerManagerSelector: ITelegramHandlerManagerSelector
{
    public ITelegramUpdateHandlerManager? GetManager(GameArc arc)
    {
        return arc switch
        {
            GameArc.StartArc => new StartUpdateHandlerManager(),
            _ => throw new NullReferenceException("Not found UpdateHandlerManager")
        };
    }
}
