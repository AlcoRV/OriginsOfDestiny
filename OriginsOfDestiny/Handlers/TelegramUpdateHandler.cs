using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.Storage;
using OriginsOfDestiny.Selectors;
using OriginsOfDestiny.StartArc.Models.MessageHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Handlers;

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly IPlayerContextManager _playerContextManager;
    private readonly ITimerHandler _timerHandler;

    public TelegramUpdateHandler(IPlayerContextManager playerContextManager, ITimerHandler timerHandler) {  
        _playerContextManager = playerContextManager;
        _timerHandler = timerHandler;
    }

    public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        ITelegramUpdateHandlerManager manager;
        IGameData gameData = new GameData();
        gameData.ClientData.PlayerContext = _playerContextManager.GetContext(update.Message?.Chat.Id ?? update.CallbackQuery!.From.Id);
        gameData.ClientData.BotClient ??= botClient;
        gameData.ClientData.TimerHandler ??= _timerHandler;

        manager = new TelegramHandlerManagerSelector().GetManager(gameData.ClientData.PlayerContext.Arc)!;

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            if (gameData.ClientData.WaitingForMessage != null)
            {
                gameData.ClientData.WaitingForMessage?.Handle(update.Message!);
                return;
            }

            if (!string.IsNullOrWhiteSpace(update.Message.Text)
                && update.Message.Text.Equals("/start", StringComparison.OrdinalIgnoreCase))
            {
                await new StartMessageHandler().Handle(gameData, update.Message);
            }
            else if(gameData.ClientData.DefaultMessageHandler != null)
            {
                await gameData.ClientData.DefaultMessageHandler.Handle(gameData, update.Message);
            }

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
