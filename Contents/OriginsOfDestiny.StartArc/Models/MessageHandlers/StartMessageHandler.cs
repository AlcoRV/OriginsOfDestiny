using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using OriginsOfDestiny.Common.UI;
using OriginsOfDestiny.Game.Objects.Opponents.ElementSpirits;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers
{
    using ArcConstants = Constants.Constants;

    public class StartMessageHandler : IMessageHandler
    {
        public async Task Handle(IGameData gameData, Message message)
        {
            var resourceHelper = new ResourceHelper<StartMessageHandler>();

            await gameData.ClientData.SendMessageAsync(
                resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Out.Start),
                true
                );

            gameData.ClientData.RiddenMessagesCodes = new HashSet<string>();
            gameData.ClientData.DefaultMessageHandler = new SimonStartDefaultMessageHandler();
            gameData.ClientData.PlayerContext.Area = TemporaryTestData.TemporaryTestData.DownEAForest;
            gameData.ClientData.PlayerContext.Opponent = HighElementSpirits.Simon;

            await gameData.ClientData.SendMessageAsync(resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Out.WokeUp));

            await gameData.ClientData.SendPhotoAsync(
                resourceHelper.GetValue(ArcConstants.Messages.SimonStart.Simon.OneMore),
                new InlineKeyboardMarkup(
                    new[]
                    {
                        UITools.GetButton<StartMessageHandler>(ArcConstants.Messages.SimonStart.Me.Who),
                        UITools.GetButton<StartMessageHandler>(ArcConstants.Messages.SimonStart.Me.How),
                        UITools.GetButton<StartMessageHandler>(ArcConstants.Messages.SimonStart.Me.Where)
                    }
                    .Chunk(1))
                );
        }
    }
}
