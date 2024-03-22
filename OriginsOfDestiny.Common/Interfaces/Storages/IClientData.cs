using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Models.WaitingFor;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Common.Interfaces.Storages;

public interface IClientData
{
    public long Id { get; set; }
    public ITelegramBotClient BotClient { get; set; }
    public WaitingForBaseMessageHandler WaitingForMessage { get; set; }
    public IPlayerContext PlayerContext { get; set; }
    public ITimerHandler TimerHandler { get; set; }
    public IEnumerable<string> AvailablesCodes { get; set; }
    public IMessageHandler DefaultMessageHandler { get; set; }
    public string LastCode { get; set; }
    public IEnumerable<string> RiddenMessagesCodes { get; set; }
    public Message MainMessage { set; get; }

    public void Clear();
    public Task EditMainMessageAsync(Message message = null, string caption = null, InlineKeyboardMarkup replyMarkup = null);
    public Task SendPhotoAsync(string caption, InlineKeyboardMarkup replyMarkup = null);
    public Task SendMessageAsync(string message, bool restartButton = false);
}
