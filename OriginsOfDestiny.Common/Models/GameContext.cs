using OriginsOfDestiny.Game.Enums;
using OriginsOfDestiny.Game.Models.Entity;
using Telegram.Bot;

namespace OriginsOfDestiny.Common.Models;

public class GameContext
{
    public ITelegramBotClient BotClient { get; set; }
    public MainHero MainHero { get; set; } = new MainHero();
    public WaitingForBaseMessageHandler? WaitingForMessage {  get; set; }
    public GameArc Arc { get; set; }
}
