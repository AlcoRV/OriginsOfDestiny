using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Models.WaitingFor;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.Common.Models.Storage;

public class ClientData : IClientData
{
    public ITelegramBotClient BotClient { get; set; }
    public WaitingForBaseMessageHandler WaitingForMessage { get; set; }
    public IPlayerContext PlayerContext { get; set; } = new PlayerContext();
    public ITimerHandler TimerHandler { get; set; }
    public IEnumerable<string> AvailablesCodes { get; set; } = new HashSet<string>();
    public IMessageHandler DefaultMessageHandler {  get; set; }
    public string LastCode {  get; set; }
    public IEnumerable<string> RiddenMessagesCodes { set; get; } = new HashSet<string>();
    public Message MainMessage { set; get; }
    public long Id { get; set; }

    public void Clear()
    {
        WaitingForMessage = null;
        (AvailablesCodes as HashSet<string>).Clear();
        DefaultMessageHandler = null;
        (RiddenMessagesCodes as HashSet<string>).Clear();
    }

    public async Task EditMainMessageAsync(Message message = null, string caption = null, InlineKeyboardMarkup replyMarkup = null)
    {
        MainMessage = await BotClient.EditMessageCaptionAsync(Id,
                 messageId: message == null ? MainMessage.MessageId : message.MessageId,
                 caption: caption == null ? MainMessage.Text : caption,
                 replyMarkup: replyMarkup
                );
    }
}
