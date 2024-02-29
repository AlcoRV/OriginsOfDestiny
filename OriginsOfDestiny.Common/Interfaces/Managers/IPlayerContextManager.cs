using OriginsOfDestiny.Common.Interfaces.Storages;

namespace OriginsOfDestiny.Common.Interfaces.Managers;

public interface IPlayerContextManager
{
    public IPlayerContext GetContext(long id);
}
