using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Utility
{
    internal class Logger
    {
        static Logger()
        {
        }

        public static void Introduce(string channel) 
        {
        
        }

        public static void WriteLog(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
        public static void WriteLog(string message, string user)
        {
            Console.WriteLine($"{DateTime.Now}: {user} - {message}");
        }

        public static void WriteErrorLog(string message)
        {
            Console.Error.WriteLine($"{DateTime.Now}: {message}");
        }

        public static void WriteErrorLog(string message, string user)
        {
            Console.Error.WriteLine($"{DateTime.Now}: {user} - {message}");
        }

    }
}
