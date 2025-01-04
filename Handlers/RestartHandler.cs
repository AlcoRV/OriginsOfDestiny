using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using OriginsOfDestiny.Services;

namespace OriginsOfDestiny.Handlers
{
    public class RestartHandler : IRestartHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ISessionService _sessionService;
        private readonly IStartService _startHandler;

        public RestartHandler(ITelegramBotClient botClient, ISessionService sessionService, IStartService startHandler)
        {
            _botClient = botClient;
            _sessionService = sessionService;
            _startHandler = startHandler;
        }

        public async Task HandleCallback(CallbackQuery query, CancellationToken token)
        {
            var chatId = query.From.Id;
            var command = query.Data;

            var session = await _sessionService.GetOrCreate(chatId);

            if (session == null) { return; }

            
            if (command.Equals(@"restart_confirm"))
            {
                await _botClient.SendMessage(
                    chatId,
                    "Сессия перезапущена",
                    cancellationToken: token);

                await _sessionService.Remove(chatId);
                await _startHandler.Start(chatId, token);
            }
            else
            {
                await _botClient.SendMessage(
                    chatId,
                    "Перезапуск отменён",
                    cancellationToken: token);
            }
        }

        public async Task HandleMessage(Message message, CancellationToken token)
        {
            if (message.Text.Equals(@"\restart"))
            {
                await _botClient.SendMessage(
                    message.Chat.Id,
                    "Вы уверены, что хотите начать сначала? Прогресс будет утерян",
                    replyMarkup: new InlineKeyboardMarkup(new[] {
                        InlineKeyboardButton.WithCallbackData("Да", "restart_confirm"),
                        InlineKeyboardButton.WithCallbackData("Нет", "restart_cancel")
                    })
                ,
                    cancellationToken: token);
            }
        }
    }
}
