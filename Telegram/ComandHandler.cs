using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.Telegram
{
    public class ComandHandler: IComandHandler
    {
        private readonly ITelegramBotClient _botClient;

        public ComandHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleUpdateAsync(Message message, CancellationToken token)
        {
            await _botClient.SendMessage(message.Chat.Id, message.Text!, cancellationToken: token);
        }
    }
}
