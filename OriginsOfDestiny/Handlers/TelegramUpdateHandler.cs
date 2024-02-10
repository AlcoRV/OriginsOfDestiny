using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Models;
using OriginsOfDestiny.Common.Models.Storage;
using OriginsOfDestiny.Selectors;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Handlers;

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly GameDataStorage _gameDataStorage;

    public TelegramUpdateHandler(GameDataStorage gameDataStorage) {  
        _gameDataStorage = gameDataStorage;
    }

    public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        GameContext gameContext;
        ITelegramUpdateHandlerManager manager;
        
        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            gameContext = _gameDataStorage.PlayerContextsStorage.GetContext(update.Message!.Chat.Id);
            gameContext.BotClient ??= botClient;
            manager = new TelegramHandlerManagerSelector().GetManager(gameContext.Arc)!;

            if (gameContext.WaitingForMessage != null)
            {
                gameContext.WaitingForMessage?.Handle(update.Message);
                return;
            }

            await manager.GetMessageHandler(update.Message!.Text!).Handle(gameContext, update.Message);

            return;
        }

        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            gameContext = _gameDataStorage.PlayerContextsStorage.GetContext(update.CallbackQuery!.From.Id);
            gameContext.BotClient ??= botClient;
            manager = new TelegramHandlerManagerSelector().GetManager(gameContext.Arc)!;

            await manager.GetCallbackQueryHandler(update.CallbackQuery.Data!).Handle(gameContext, update.CallbackQuery);

            return;
        }

    }
}
