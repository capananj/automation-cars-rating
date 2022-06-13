using Microsoft.Extensions.Configuration;
using System;

namespace BuggyCars.Settings
{
    public class AppSettingsLoader
    {
        public readonly IConfiguration Configuration;

        public AppSettingsLoader()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }
    }
}
