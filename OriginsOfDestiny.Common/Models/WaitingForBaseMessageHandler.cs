using Telegram.Bot.Types;

namespace OriginsOfDestiny.Common.Models;

public abstract class WaitingForBaseMessageHandler
{
    protected GameContext? GameContext { get; private init; }

    public static class Factory
    {
        public static T Create<T>(GameContext gameContext) where T : WaitingForBaseMessageHandler, new()
        {
            return new T() { GameContext = gameContext };
        }
    }

    public abstract Task Handle(Message message); 
}
