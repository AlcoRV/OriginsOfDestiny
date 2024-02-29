using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Game.Enums;

namespace OriginsOfDestiny.Interfaces;

public interface ITelegramHandlerManagerSelector
{
    public ITelegramUpdateHandlerManager? GetManager(GameArc arc);
}
