using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Common.Models.WaitingFor;

public abstract class WaitingForBaseMessageHandler
{
    protected IGameData? GameData { get; private init; }

    public static class Factory
    {
        public static T Create<T>(IGameData gameData) where T : WaitingForBaseMessageHandler, new()
        {
            return new T() { GameData = gameData };
        }
    }

    public abstract Task Handle(Message message);
}
