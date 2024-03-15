using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.Storage;

namespace OriginsOfDestiny.Common.Managers;

public class ClientDataManager: IClientDataManager
{
    private readonly Dictionary<long, IClientData> _clientContexts = new();

    /// <summary>
    ///     Getting client data
    /// </summary>
    /// <param name="id">Client data id</param>
    /// <param name="clientData">Client data</param>
    /// <returns>true - if new client data created</returns>
    public bool GetOrCreateContext(long id, out IClientData clientData)
    {
        if (_clientContexts.TryGetValue(id, out clientData))
        {
            return false;
        }
        else
        {
            clientData = new ClientData() { Id = id };
            _clientContexts.Add(id, clientData);
            return true;
        }
    }
}
