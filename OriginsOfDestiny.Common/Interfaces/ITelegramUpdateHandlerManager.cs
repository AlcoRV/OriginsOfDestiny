namespace OriginsOfDestiny.Common.Interfaces;

public interface ITelegramUpdateHandlerManager
{
    public IMessageHandler GetMessageHandler(string message);
    public ICallbackQueryHandler GetCallbackQueryHandler(string callbackMessage);
}
