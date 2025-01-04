using OriginsOfDestiny.Services;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Handlers
{
    public class StartHandler : IStartService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ISessionService _sessionService;
        private readonly IDialogService _dialogService;

        public StartHandler(ITelegramBotClient botClient, ISessionService sessionService, IDialogService dialogService)
        {
            _botClient = botClient;
            _sessionService = sessionService;
            _dialogService = dialogService;
        }

        public Task HandleCallback(CallbackQuery query, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task HandleMessage(Message message, CancellationToken token)
        {
            var session = await _sessionService.GetOrCreate(message.Chat.Id);

            if (session.ActiveDialog == null && !message.Text.Equals(@"\start")) { return; }

            var activeDialog = await _dialogService.Get(session.ActiveDialogId);

            await _botClient.SendMessage(message.Chat.Id, session.ActiveDialog.Text, replyMarkup: CreateInlineKeyboardMarkup(activeDialog.Responses), cancellationToken: token);
        }

        public async Task Start(long id, CancellationToken cancellationToken)
        {
            var session = await _sessionService.GetOrCreate(id);
            var activeDialog = await _dialogService.Get(session.ActiveDialogId);

            await _botClient.SendMessage(id, session.ActiveDialog.Text, replyMarkup: CreateInlineKeyboardMarkup(activeDialog.Responses), cancellationToken: cancellationToken);
        }

        private InlineKeyboardMarkup CreateInlineKeyboardMarkup(Dictionary<string, string> responses)
        {
            if (responses == null || !responses.Any()) return null;

            var buttons = responses.Select(r => new[] { InlineKeyboardButton.WithCallbackData(r.Value, r.Key) });
            return new InlineKeyboardMarkup(buttons);
        }

        private ReplyKeyboardMarkup CreateReplyKeyboardMarkup(string buttonText)
        {
            return new ReplyKeyboardMarkup(new[] { new KeyboardButton(buttonText) }) { ResizeKeyboard = true };
        }
    }
}
