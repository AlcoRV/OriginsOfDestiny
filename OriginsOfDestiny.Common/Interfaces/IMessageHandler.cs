using Telegram.Bot.Types;
using OriginsOfDestiny.Common.Models;

namespace OriginsOfDestiny.Common.Interfaces;

public interface IMessageHandler {
    public Task Handle(GameContext context, Message message);
}
