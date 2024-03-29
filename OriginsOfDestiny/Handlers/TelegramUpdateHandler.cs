using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Managers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.Storage;
using OriginsOfDestiny.DataObjects.Interfaces.Items;
using OriginsOfDestiny.DataObjects.Models.Items.Pickups;
using OriginsOfDestiny.Selectors;
using OriginsOfDestiny.StartArc.Models.MessageHandlers;
using OriginsOfDestiny.StartArc.TemporaryTestData;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Handlers;

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly IClientDataManager _playerContextManager;
    private readonly ITimerHandler _timerHandler;

    public TelegramUpdateHandler(IClientDataManager clientDataManager, ITimerHandler timerHandler) {  
        _playerContextManager = clientDataManager;
        _timerHandler = timerHandler;
    }

    public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        IGameData gameData = BuildGameData(botClient, update.Message?.Chat.Id ?? update.CallbackQuery!.From.Id);

        var manager = new TelegramHandlerManagerSelector().GetManager(gameData.ClientData.PlayerContext.Arc)!;

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            await MessageUpdate(update.Message!, gameData);
        }

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            if (!gameData.ClientData.AvailablesCodes.Contains(update.CallbackQuery!.Data)) { return; }

            await manager.GetCallbackQueryHandler(update.CallbackQuery!.Data!).Handle(gameData, update.CallbackQuery);
            gameData.ClientData.LastCode = update.CallbackQuery.Data;
        }
    }

    private static async Task MessageUpdate(Message message, IGameData gameData)
    {
        if (gameData.ClientData.WaitingForMessage != null)
        {
            gameData.ClientData.WaitingForMessage?.Handle(message);
            return;
        }

        if (string.IsNullOrWhiteSpace(message.Text)) { return; }

        if (message.Text.Equals("/start", StringComparison.OrdinalIgnoreCase)
                || message.Text.Equals("/restart", StringComparison.OrdinalIgnoreCase))
        {
            await new StartMessageHandler().Handle(gameData, message);
        }
        else if(message.Text.Equals("/test", StringComparison.OrdinalIgnoreCase))
        {
            await new TestMessageHandler().Handle(gameData, message);
        }
        else if (gameData.ClientData.DefaultMessageHandler != null)
        {
            await gameData.ClientData.DefaultMessageHandler.Handle(gameData, message);
        }
    }

    private IGameData BuildGameData(ITelegramBotClient botClient, long clientId)
    {
        IGameData gameData = new GameData();
        if(_playerContextManager.GetOrCreateContext(clientId, out var clientData))
        {
            clientData.BotClient = botClient;
            clientData.TimerHandler = _timerHandler;
        }
        gameData.ClientData = clientData;

        ChangeDataForTest(gameData);

        return gameData;
    }

    private static void ChangeDataForTest(IGameData gameData)
    {
        gameData.ClientData.PlayerContext.Hero.Name = "Alex";
        gameData.ClientData.PlayerContext.Hero.Gender = DataObjects.Enums.Gender.Man;
        gameData.ClientData.PlayerContext.Hero.HP = 100;
        gameData.ClientData.PlayerContext.Hero.MaxHP = 100;
        gameData.ClientData.PlayerContext.Hero.Element = DataObjects.Enums.Element.Fire;
        gameData.ClientData.PlayerContext.Hero.Influences.Effects[DataObjects.Enums.Element.Fire] = 1.25;
        gameData.ClientData.PlayerContext.Hero.Influences.Effects[DataObjects.Enums.Element.Water] = 0.75;
        var inventory = gameData.ClientData.PlayerContext.Hero.Inventory as HashSet<IItem>;
        inventory.Add(new Note()
        {
            Name = "Таинственная записка 📜",
            Description = "Какое-то описание странной записки. П.с. ОТ ДРУГА",
            Empty = false
        });
        inventory.Add(new Note()
        {
            Name = "Бумага 📜",
            Description = "",
            Empty = true
        });

        gameData.ClientData.PlayerContext.Area = TemporaryTestData.DownEAForest;
    }
}
