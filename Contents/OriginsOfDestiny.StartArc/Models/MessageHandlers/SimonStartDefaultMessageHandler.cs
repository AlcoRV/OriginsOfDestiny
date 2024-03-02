using OriginsOfDestiny.Common.Helpers;
using OriginsOfDestiny.Common.Interfaces.Handlers;
using OriginsOfDestiny.Common.Interfaces.Storages;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OriginsOfDestiny.StartArc.Models.MessageHandlers
{
    using Constants = Constants.Constants;

    public class SimonStartDefaultMessageHandler : IMessageHandler
    {
        public async Task Handle(IGameData gameData, Message message)
        {
            var resourceHelper = new ResourceHelper<SimonStartDefaultMessageHandler>();

            var messageText = "";
            switch (new Random().Next(3))
            {
                case 0: messageText = resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Fool); break;
                case 1: messageText = resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Calm); break;
                case 2: messageText = resourceHelper.GetValue(Constants.Messages.SimonStart.Simon.Kill); break;

                default: break;
            }

            var messageId = (await gameData.ClientData.BotClient.SendTextMessageAsync(message.Chat.Id, messageText)).MessageId;

            Thread.Sleep(2000);

            await gameData.ClientData.BotClient.DeleteMessageAsync(message.Chat.Id, messageId);
            await gameData.ClientData.BotClient.DeleteMessageAsync(message.Chat.Id, message.MessageId);
        }
    }
}
