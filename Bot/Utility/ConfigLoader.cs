﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Utility
{
    internal class ConfigLoader
    {
        public string Token { get; private set; }
        public string Prefix { get; private set; }
        public ulong MainChannelID { get; private set; }
        public ulong SecondaryChannelID { get; private set; }

        public ConfigLoader(IConfiguration configuration)
        {
            Token = configuration["Token"] ?? throw new InvalidOperationException("Token is missing in the configuration.");
            Prefix = configuration["Prefix"] ?? throw new InvalidOperationException("Prefix is missing in the configuration.");
            MainChannelID = ulong.Parse(configuration["MainChannel"] ?? throw new InvalidOperationException("MainChannelID is missing in the configuration."));
            SecondaryChannelID = ulong.Parse(configuration["SecondaryChannel"] ?? throw new InvalidOperationException("SecondaryChannelID is missing in the configuration."));
        }
    }
}