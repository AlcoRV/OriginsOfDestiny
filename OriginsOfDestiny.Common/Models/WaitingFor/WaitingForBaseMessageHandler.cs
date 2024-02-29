using Telegram.Bot.Types;

namespace OriginsOfDestiny.Common.Models.WaitingFor;

public abstract class WaitingForBaseMessageHandler
{
    protected PlayerContext? GameContext { get; private init; }

    public static class Factory
    {
        public static T Create<T>(PlayerContext gameContext) where T : WaitingForBaseMessageHandler, new()
        {
            return new T() { GameContext = gameContext };
        }
    }

    public abstract Task Handle(Message message);
}
