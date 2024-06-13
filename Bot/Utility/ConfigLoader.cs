using Discord;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Utility
{
    public class ConfigLoader
    {
        public string Token { get; private set; }
        public string Prefix { get; private set; }
        public ulong MainChannelID { get; private set; }
        public ulong SecondaryChannelID { get; private set; }
        private readonly Logger _logger;

        // Class that loads configuration from appsettings.json
        public ConfigLoader(IConfiguration configuration, Logger logger)
        {
            Token = configuration["Token"] ?? throw new InvalidOperationException("Token is missing in the configuration.");
            Prefix = configuration["Prefix"] ?? throw new InvalidOperationException("Prefix is missing in the configuration.");
            MainChannelID = ulong.Parse(configuration["MainChannel"] ?? throw new InvalidOperationException("MainChannelID is missing in the configuration."));
            SecondaryChannelID = ulong.Parse(configuration["SecondaryChannel"] ?? throw new InvalidOperationException("SecondaryChannelID is missing in the configuration."));
            
            _logger = logger;
            _logger.LogAsync(new LogMessage(LogSeverity.Info, "ConfigLoader", "Configuration loaded successfully."));
        }
    }
}
