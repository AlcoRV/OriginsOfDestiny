using System.Resources;

namespace OriginsOfDestiny.Common.Helpers;

public class ResourceHelper<T>
{
    private readonly ResourceManager _resourceManager;

    public ResourceHelper()
    {
        _resourceManager = new ResourceManager(typeof(T));
    }

    public string GetValue(string key)
    {
        var culture = Thread.CurrentThread.CurrentCulture;

        return _resourceManager.GetString(key, culture);
    }
}
