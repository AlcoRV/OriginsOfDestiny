using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Managers;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.DataObjects.Enums;
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
                 caption: caption == null ? MainMessage.Text : GetCaption(caption),
                 replyMarkup: replyMarkup
                );
    }

    public async Task SendMessageAsync(string message, bool restartButton = false)
    {
        var replyMarkup = restartButton
            ? new ReplyKeyboardMarkup(
                new KeyboardButton("/restart")
                )
            {
                ResizeKeyboard = true
            }
            : null;

        await BotClient.SendTextMessageAsync(
            Id, 
            message,
            replyMarkup: replyMarkup);
    }

    public async Task SendPhotoAsync(string caption, InlineKeyboardMarkup replyMarkup = null)
    {
        var picture = PlayerContext.Opponent != null
            ? PlayerContext.Opponent.Picture
            : PlayerContext.Area.Picture;

        using var fileStream = new FileManager().GetFileStream(picture);

        MainMessage = await BotClient.SendPhotoAsync(Id,
                    new InputFileStream(fileStream),
                    caption: GetCaption(caption),
                    replyMarkup: replyMarkup
                );
    }

    private string GetCaption(string caption)
    {
        return PlayerContext.Opponent == null || PlayerContext.Opponent.Attitude == Attitude.Hostile
            ? $"{PlayerContext.GetHeroHealth()}\n{caption}"
            : caption;
    }
}
