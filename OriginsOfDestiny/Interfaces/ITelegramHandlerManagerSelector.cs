using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Data.Enums;

namespace OriginsOfDestiny.Interfaces;

public interface ITelegramHandlerManagerSelector
{
    public ITelegramUpdateHandlerManager? GetManager(GameArc arc);
}
