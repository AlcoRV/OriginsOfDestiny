using Telegram.Bot.Types;
using OriginsOfDestiny.Common.Interfaces.Storages;

namespace OriginsOfDestiny.Common.Interfaces.Handlers;

public interface IMessageHandler
{
    public Task Handle(IGameData gameData, Message message);
}
