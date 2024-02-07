﻿using Microsoft.Extensions.Configuration;

namespace OriginsOfDestiny.Common.Providers;

public class ConfigurationProvider
{
    public IConfiguration GetConfiguration() =>
        new ConfigurationBuilder()
        .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
        .AddJsonFile("Properties/appsettings.json", optional: true, reloadOnChange: true)
        .Build();
}
