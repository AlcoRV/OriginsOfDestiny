﻿using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.Managers;
using OriginsOfDestiny.Common.Models.WaitingFor;
using OriginsOfDestiny.DataObjects.Enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
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
        DefaultMessageHandler = null;
        (RiddenMessagesCodes as HashSet<string>).Clear();
    }

    public async Task EditMainMessageAsync(Message message = null, string caption = null, InlineKeyboardMarkup replyMarkup = null)
    {
        if(PlayerContext.Hero.HP == 0)
        {
            replyMarkup = null;
            AvailablesCodes = new HashSet<string>();
        }

        if (replyMarkup != null)
        {
            AvailablesCodes = replyMarkup.InlineKeyboard.SelectMany(el => el.Select(el => el.CallbackData ?? ""));
        }

        try
        {

            MainMessage = await BotClient.EditMessageCaptionAsync(Id,
                     messageId: message == null ? MainMessage.MessageId : message.MessageId,
                     caption: caption == null ? MainMessage.Text : GetCaption(caption),
                     replyMarkup: replyMarkup
                    );

        }
        catch(ApiRequestException e)
        {
            var resourceHelper = new ResourceHelper<ClientData>();

            caption = $"{(caption == null ? MainMessage.Text : GetCaption(caption))}\n" +
            $"{resourceHelper.GetValue("API_REQUEST_EXCEPTION")}";

            Thread.Sleep(200);
            MainMessage = await BotClient.EditMessageCaptionAsync(Id,
                     messageId: message == null ? MainMessage.MessageId : message.MessageId,
                     caption:  caption,
                     replyMarkup: replyMarkup
                    );
        }
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
        AvailablesCodes = replyMarkup.InlineKeyboard.SelectMany(el => el.Select(el => el.CallbackData ?? ""));

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
