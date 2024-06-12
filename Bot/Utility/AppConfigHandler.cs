using Discord;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Utility
{
    internal class AppConfigHandler
    {
        private ConfigurationBuilder builder;
        public string? Token { get; private set; }
        public string? Prefix { get; private set; }
        public ulong MainChannelID { get; private set; }
        public ulong SecondaryChannelID { get; private set; }

        private static AppConfigHandler? appConfigHandler;

        public static AppConfigHandler Instance
        {
            get
            {
                appConfigHandler ??= new AppConfigHandler();
                return appConfigHandler;
            }
        }

        private AppConfigHandler()
        {
            Token = string.Empty;
            Prefix = string.Empty;
            MainChannelID = 0;
            SecondaryChannelID = 0;
            builder = new();
            builder.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json");
        }

        public void Initialize()
        {
            LoadValues();
        }

        private void LoadValues()
        {
            bool allInitialized = CheckValuePresence();
            if (!allInitialized)
            {
                Logger.LogAsync(new LogMessage(LogSeverity.Critical, "AppConfigHandler", "One or more values are missing in appsettings.json"));
                Environment.Exit(1);
            }
            Console.WriteLine("Values from appconfig.json loaded successfully");
        }

        private bool CheckValuePresence()
        {
            IConfiguration config = builder.Build();
            bool allInitialized = true;

            if (config["Token"] == null)
            {
                Logger.LogAsync(new LogMessage(LogSeverity.Critical, "AppConfigHandler", "All values not found in appsettings.json"));
                allInitialized = false;
            }
            if (config["Prefix"] == null)
            {
                Logger.LogAsync(new LogMessage(LogSeverity.Critical, "AppConfigHandler", "Prefix not found in appsettings.json"));
                allInitialized = false;
            }
            if (config["MainChannel"] == null)
            {
                Logger.LogAsync(new LogMessage(LogSeverity.Critical, "AppConfigHandler", "MainChannel not found in appsettings.json"));
                allInitialized = false;
            }
            if (config["SecondaryChannel"] == null)
            {
                Logger.LogAsync(new LogMessage(LogSeverity.Critical, "AppConfigHandler", "SecondaryChannel not found in appsettings.json"));
                allInitialized = false;
            }

            if (!allInitialized)
                return false;

            Token = config["Token"];
            Prefix = config["Prefix"];
            string? mainChannel = config["MainChannel"];
            string? secondaryChannel = config["MainChannel"];

            if (mainChannel == null || secondaryChannel == null)
                return false;

            MainChannelID = ulong.Parse(mainChannel);
            SecondaryChannelID = ulong.Parse(secondaryChannel);
            return true;
        }
    }
}
