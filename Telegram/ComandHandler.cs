using Microsoft.EntityFrameworkCore;
using OriginsOfDestiny.Handlers;
using OriginsOfDestiny.Models.Dialogs;
using OriginsOfDestiny.Models.Sessions;
using OriginsOfDestiny.Repositories;
using OriginsOfDestiny.Services;
using StackExchange.Redis;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Telegram
{
    public class ComandHandler : IComandHandler
    {
        private readonly ITelegramBotClient _botClient;
        //private readonly IConnectionMultiplexer _redis;
        private readonly IRepository<UserSession> _repository;
        private readonly ISessionService _sessionService;
        private readonly IDialogService _dialogService;
        private readonly IMenuService _menuService;
        private readonly IRestartHandler _restartHandler;
        private readonly IStartService _startHandler;

        private readonly Dictionary<string, ICallbackHandler> _callbackHandlers;
        private readonly Dictionary<string, IMessageHandler> _messageHandlers;

        public ComandHandler(ITelegramBotClient botClient, /*IConnectionMultiplexer redis, */IRepository<UserSession> repository,
            ISessionService sessionService,
            IDialogService dialogService,
            IMenuService menuService,
            IRestartHandler restartHandler,
            IStartService startHandler)
        {
            _botClient = botClient;
            //_redis = redis;
            _repository = repository;
            _sessionService = sessionService;
            _dialogService = dialogService;
            _menuService = menuService;
            _restartHandler = restartHandler;
            _startHandler = startHandler;

            _callbackHandlers = new Dictionary<string, ICallbackHandler>()
            {
                { "restart", _restartHandler }
            };

            _messageHandlers = new Dictionary<string, IMessageHandler>()
            {
                { @"\restart", _restartHandler },
                { @"\start", _startHandler },
            };
        }

        public async Task HandleCallbackQueryUpdateAsync(CallbackQuery query, CancellationToken token)
        {
            var command = query.Data.Split('_')[0];

            if (_callbackHandlers.TryGetValue(command, out var callbackHandler))
            {
                await callbackHandler.HandleCallback(query, token);
            }
            else
            {
                var session = await _sessionService.GetOrCreate(query.From.Id);

                var activeDialog = await _dialogService.Get(query.Data);
                session.ActiveDialogId = activeDialog?.Responses == null || !activeDialog.Responses.Any()
                    ? null!
                    : activeDialog.Id;

                if (session.ActiveDialogId == null)
                {
                    await _menuService.HandleCallback(query, token);
                }
                else
                {
                    await ReplyByDialog(query, activeDialog);
                    await _sessionService.Udpate(session);
                }
            }

            
        }

        private async Task ReplyByDialog(CallbackQuery query, Dialog activeDialog)
        {
            var buttons = activeDialog.Responses.Select(r => new[] { InlineKeyboardButton.WithCallbackData(r.Value, r.Key) });
            var markup = new InlineKeyboardMarkup(buttons);

            if (activeDialog.NeedReplace)
            {
                await _botClient.EditMessageText(
                    chatId: query.From.Id,
                    messageId: query.Message.Id, // Убедитесь, что используется правильное свойство
                    activeDialog.Text,
                    replyMarkup: markup
                );
            }
            else
            {
                await _botClient.SendMessage(query.From.Id, activeDialog.Text, replyMarkup: markup);
            }
        }

        public async Task HandleMessageUpdateAsync(Message message, CancellationToken token)
        {
            if (_messageHandlers.TryGetValue(message.Text, out var messageHandler)){
                await messageHandler.HandleMessage(message, token);
                return;
            }
            else
            {
                ///
            }

            /*

            var db = _redis.GetDatabase();

            var index = 0;

            int.TryParse(await db.StringGetAsync(message.Chat.Id.ToString()), out index);

            await _botClient.SendMessage(message.Chat.Id, message.Text! + index, cancellationToken: token);

            index++;
            await db.StringSetAsync(message.Chat.Id.ToString(), index, TimeSpan.FromMinutes(1));
            */
        }
    }
}
