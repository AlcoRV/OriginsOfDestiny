using OriginsOfDestiny.Data;
using OriginsOfDestiny.Models.Dialogs;
using OriginsOfDestiny.Repositories;
using StackExchange.Redis;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OriginsOfDestiny.Telegram
{
    public class ComandHandler: IComandHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IConnectionMultiplexer _redis;
        private readonly IRepository<Dialog> _repository;

        public ComandHandler(ITelegramBotClient botClient, IConnectionMultiplexer redis, IRepository<Dialog> repository)
        {
            _botClient = botClient;
            _redis = redis;
            _repository = repository;
        }

        public async Task HandleCallbackQueryUpdateAsync(CallbackQuery query, CancellationToken token)
        {
            var dialog = _repository.Get(d => d.Id == query.Data).FirstOrDefault();
            if (dialog == null) { return; }

            var buttons = dialog.Responses.Select(r => new[] { InlineKeyboardButton.WithCallbackData(r.Value, r.Key) });

            await _botClient.SendMessage(query.From.Id, dialog.Text, replyMarkup: new InlineKeyboardMarkup(buttons));
        }

        public async Task HandleMessageUpdateAsync(Message message, CancellationToken token)
        {
            var dialog = _repository.Get(d => d.Id == message.Text).FirstOrDefault();
            if (dialog == null) { return; }

            var buttons = dialog.Responses.Select(r => new[] { InlineKeyboardButton.WithCallbackData(r.Value, r.Key) });

            await _botClient.SendMessage(message.Chat.Id, dialog.Text, replyMarkup: new InlineKeyboardMarkup(buttons));

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
