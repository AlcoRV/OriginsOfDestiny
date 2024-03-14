namespace OriginsOfDestiny.Common.Interfaces.Handlers;

public interface ITimerHandler
{
    public void Start(string key, Action<object> callback, TimeSpan timeSpan);
    public void Stop(string key);
}
