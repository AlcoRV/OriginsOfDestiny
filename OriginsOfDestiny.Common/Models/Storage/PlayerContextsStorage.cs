namespace OriginsOfDestiny.Common.Models.Storage;

public class PlayerContextsStorage
{
    private readonly Dictionary<long, GameContext> _gameContexts = new();

    public GameContext GetContext(long id)
    {
        if (_gameContexts.TryGetValue(id, out var context))
        {
            return context;
        }
        else
        {
            context = new GameContext();
            _gameContexts.Add(id, context);
            return context;
        }
    }
}
