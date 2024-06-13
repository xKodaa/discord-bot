using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Utility
{
    public static class Util
    {
        public static string GetProjectRoot()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var projectRoot = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
            return projectRoot ?? AppContext.BaseDirectory;
        }

        public static string GetDataPath()
        {
            var root = GetProjectRoot();
            return Path.Combine(root, "Data");
        }
    }
}
