using OriginsOfDestiny.Common.Models.WaitingFor;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IClientData
{
    public ITelegramBotClient BotClient { get; set; }
    public WaitingForBaseMessageHandler WaitingForMessage { get; set; }
    public IPlayerContext PlayerContext { get; set; }
}
