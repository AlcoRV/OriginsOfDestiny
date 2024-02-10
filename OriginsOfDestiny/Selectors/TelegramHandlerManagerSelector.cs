using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Game.Enums;
using OriginsOfDestiny.StartArc.Managers;

namespace OriginsOfDestiny.Selectors;

public class TelegramHandlerManagerSelector
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
