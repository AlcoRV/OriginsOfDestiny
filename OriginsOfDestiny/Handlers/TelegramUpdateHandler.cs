using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.Storage;
using OriginsOfDestiny.Selectors;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Handlers;

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly IPlayerContextManager _playerContextManager;

    public TelegramUpdateHandler(IPlayerContextManager playerContextManager) {  
        _playerContextManager = playerContextManager;
    }

    public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        ITelegramUpdateHandlerManager manager;
        IGameData gameData = new GameData();
        gameData.ClientData.PlayerContext = _playerContextManager.GetContext(update.Message?.Chat.Id ?? update.CallbackQuery!.From.Id);
        gameData.ClientData.BotClient ??= botClient;

        manager = new TelegramHandlerManagerSelector().GetManager(gameData.ClientData.PlayerContext.Arc)!;

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            if (gameData.ClientData.WaitingForMessage != null)
            {
                gameData.ClientData.WaitingForMessage?.Handle(update.Message!);
                return;
            }

            await manager.GetMessageHandler(update.Message!.Text!).Handle(gameData, update.Message);

            return;
        }

        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            manager = new TelegramHandlerManagerSelector().GetManager(gameData.ClientData.PlayerContext.Arc)!;

            await manager.GetCallbackQueryHandler(update.CallbackQuery!.Data!).Handle(gameData, update.CallbackQuery);

            return;
        }

    }
}
