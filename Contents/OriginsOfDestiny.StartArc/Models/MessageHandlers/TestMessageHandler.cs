using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Game.Enums;
using OriginsOfDestiny.Game.Models.Entity;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers;

public class TestMessageHandler : IMessageHandler
{
    public async Task Handle(IGameData gameData, Message message)
    {
        await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, message.Text!);
        gameData.ClientData.PlayerContext.MainHero = new MainHero("Alex", Gender.Man);

        gameData.ClientData.TimerHandler.Start("test",
            async obj =>
            {
                await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, gameData.ClientData.PlayerContext.MainHero.Name);
            },
            new TimeSpan(0, 0, 2));
    }
}
