using Microsoft.EntityFrameworkCore;
using OriginsOfDestiny.Models.Sessions;
using OriginsOfDestiny.Repositories;
using StackExchange.Redis;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Telegram
{
    public class ComandHandler : IComandHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IConnectionMultiplexer _redis;
        private readonly IRepository<UserSession> _repository;

        public ComandHandler(ITelegramBotClient botClient, IConnectionMultiplexer redis, IRepository<UserSession> repository)
        {
            _botClient = botClient;
            _redis = redis;
            _repository = repository;
        }

        public async Task HandleCallbackQueryUpdateAsync(CallbackQuery query, CancellationToken token)
        {
            var session = _repository.Get(s => s.Id == query.From.Id)
                .FirstOrDefault();

            session.ActiveDialogId = query.Data;
            _repository.Update(session);

            session = _repository.Get(s => s.Id == query.From.Id)
                .Include(s => s.ActiveDialog)
                .FirstOrDefault();

            if (session.ActiveDialog == null) { return; }

            InlineKeyboardMarkup markup = null;
            if (session.ActiveDialog.Responses != null)
            {
                var buttons = session.ActiveDialog.Responses.Select(r => new[] { InlineKeyboardButton.WithCallbackData(r.Value, r.Key) });
                markup = new InlineKeyboardMarkup(buttons);
            }

            if (session.ActiveDialog.NeedReplace)
            {
                await _botClient.EditMessageText(
                    chatId: query.From.Id,
                    messageId: query.Message.Id, // Убедитесь, что используется правильное свойство
                    session.ActiveDialog.Text,
                    replyMarkup: markup
                );
            }
            else
            {
                await _botClient.SendMessage(query.From.Id, session.ActiveDialog.Text, replyMarkup: markup);
            }
        }

        public async Task HandleMessageUpdateAsync(Message message, CancellationToken token)
        {
            var session = _repository.Get(s => s.Id == message.Chat.Id)
                .Include(s => s.ActiveDialog)
                .FirstOrDefault();

            if (session == null)
            {
                _repository.Create(new UserSession()
                {
                    Id = message.Chat.Id
                });

                session = _repository.Get(s => s.Id == message.Chat.Id)
                    .Include(s => s.ActiveDialog)
                    .FirstOrDefault();
            }

            if (session.ActiveDialog == null) { return; }

            var buttons = session.ActiveDialog.Responses.Select(r => new[] { InlineKeyboardButton.WithCallbackData(r.Value, r.Key) });

            await _botClient.SendMessage(message.Chat.Id, session.ActiveDialog.Text, replyMarkup: new InlineKeyboardMarkup(buttons));

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
