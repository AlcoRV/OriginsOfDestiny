using OriginsOfDestiny.Common.Interfaces.Storages;

namespace OriginsOfDestiny.Common.Models.Storage;

public class GameData: IGameData
{
    public IClientData ClientData { get; set; } = new ClientData();
}
