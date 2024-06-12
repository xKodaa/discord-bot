using Microsoft.Extensions.Configuration;


namespace discord_bot.Bot
{
    internal class AuthHandler
    {
        readonly static ConfigurationBuilder builder = new();
        private static string? token;

        public static void Authenticate() 
        {
            builder.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json");
            LoadToken();
        }

        private static void LoadToken()
        {
            token = GetToken();
            if (token == null)
            {
                Console.WriteLine("Token not found in appsettings.json");
                Environment.Exit(1);
            }
            Console.WriteLine("Token loaded successfully");
        }

        private static string? GetToken()
        {
            IConfiguration config = builder.Build();
            return config["Token"];
        }
    }
}
