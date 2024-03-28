using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot.Types;
using static OriginsOfDestiny.StartArc.Constants.Constants.Messages;
using Telegram.Bot.Types.ReplyMarkups;
using OriginsOfDestiny.StartArc.Models.WaitingForHandlers.Message;
using OriginsOfDestiny.Game.Models.Actions;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers;

public class TestMessageHandler : IMessageHandler
{
    private ResourceHelper<WaitingForNameMessageHandler> _resourceHelper { get; set; } = new();
    public async Task Handle(IGameData gameData, Message message)
    {
        await gameData.ClientData.SendPhotoAsync(
            caption: _resourceHelper.GetValue(SimonStart.Out.EAF),
            replyMarkup: new InlineKeyboardMarkup(HeroActions.GetBaseActions())
        );
    }
}
