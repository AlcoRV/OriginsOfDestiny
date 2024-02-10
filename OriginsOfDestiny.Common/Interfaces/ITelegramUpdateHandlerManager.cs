namespace OriginsOfDestiny.Common.Interfaces;

public interface ITelegramUpdateHandlerManager
{
    public IMessageHandler GetMessageHandler(string code);
    public ICallbackQueryHandler GetCallbackQueryHandler(string code);
}
