using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models;

namespace OriginsOfDestiny.Common.Managers;

public class PlayerContextsManager: IPlayerContextManager
{
    private readonly Dictionary<long, IPlayerContext> _gameContexts = new();

    public IPlayerContext GetContext(long id)
    {
        if (_gameContexts.TryGetValue(id, out var context))
        {
            return context;
        }
        else
        {
            context = new PlayerContext();
            _gameContexts.Add(id, context);
            return context;
        }
    }
}
