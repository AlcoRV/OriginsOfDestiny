using OriginsOfDestiny.Game.Models;

namespace OriginsOfDestiny.Common.Models;

public class GameContext
{
    public MainHero MainHero { get; set; } = new MainHero();
    public WaitingForBaseMessageHandler? WaitingForMessage {  get; set; }
}
