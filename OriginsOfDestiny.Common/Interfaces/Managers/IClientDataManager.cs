using OriginsOfDestiny.Common.Interfaces.Storages;

namespace OriginsOfDestiny.Common.Interfaces.Managers;

public interface IClientDataManager
{
    public bool GetOrCreateContext(long id, out IClientData clientData);
}
