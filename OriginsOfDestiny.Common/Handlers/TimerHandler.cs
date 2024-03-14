using OriginsOfDestiny.Common.Interfaces.Handlers;

namespace OriginsOfDestiny.Common.Handlers;

public class TimerHandler : ITimerHandler
{
    private readonly Dictionary<string, Timer> _timers = new();

    public void Start(string key, Action<object> callback, TimeSpan timeSpan)
    {
        if(!_timers.ContainsKey(key))
        {
            TimerCallback timerCallback = new TimerCallback(callback);
            var timer = new Timer(timerCallback, null, timeSpan, timeSpan);
            _timers.Add(key, timer);
        }
    }

    public void Stop(string key)
    {
        if (!_timers.ContainsKey(key))
        {
            _timers[key].Dispose();
        }
    }
}
