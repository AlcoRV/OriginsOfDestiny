using Microsoft.Extensions.Configuration;

namespace OriginsOfDestiny.Common.Providers;

public class ConfigurationProvider
{
    public IConfiguration GetConfiguration() =>
        new ConfigurationBuilder()
        .Build();
}
