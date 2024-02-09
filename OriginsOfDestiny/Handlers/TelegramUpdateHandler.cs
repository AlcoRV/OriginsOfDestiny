using OriginsOfDestiny.Common.Interfaces;
using OriginsOfDestiny.Common.Models;
using OriginsOfDestiny.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Handlers;

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly GameContext _gameContext;

    public TelegramUpdateHandler(GameContext gameContext) {  
        _gameContext = gameContext; 
    }

    public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        if(_gameContext.WaitingForMessage != null)
        {
            _gameContext.WaitingForMessage?.Handle(botClient, update.Message);
            return;
        }

        var manager = new TelegramUpdateHandlerManager();
        
        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            await manager.GetMessageHandler(update.Message!.Text!).Handle(botClient, update.Message);
        }
        else if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            if(update.CallbackQuery!.Data == "damage")
            {
                _gameContext.MainHero.DamageSelf();
                await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, "Oooow, bolno!!!");
                await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, _gameContext.MainHero.HP.ToString() );
                return;
            }

            if (update.CallbackQuery!.Data == "setname")
            {
                await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, "Enter your name!");
                _gameContext.WaitingForMessage = WaitingForBaseMessageHandler.Factory.Create<TestWaitingForMessageName>(_gameContext);
                return;
            }

                await manager.GetCallbackQueryHandler(update.CallbackQuery.Data!).Handle(botClient, update.CallbackQuery);
        }

    }

    public class TestWaitingForMessageName : WaitingForBaseMessageHandler
    {
        public override async Task Handle(ITelegramBotClient botClient, Message message)
        {
            if (message == null)
            {
                //await botClient.SendTextMessageAsync(message.Chat.Id, "You didn't write, bastard!!!");
                return;
            }
            if (message.Text!.StartsWith("qwe"))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Your name's CAL, enter again!!!");
                return;
            }

            GameContext!.MainHero.Name = message.Text;
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Hello {GameContext.MainHero.Name}!");

            GameContext.WaitingForMessage = null!;
        }
    }
}
