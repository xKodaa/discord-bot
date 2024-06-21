using discord_bot.Bot.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Model
{
    public class SizeService
    {
        private readonly Random _random;
        private readonly string _answersPath;
        public SizeData? data;


        public SizeService()
        {
            _random = new Random();
            _answersPath = Path.Combine(Util.GetDataPath(), "dicksize.json");
            data = new SizeData();
            LoadAnswers();
        }

        private void LoadAnswers()
        {
            if (data?.Sizes == null)
            {
                var json = File.ReadAllText(_answersPath);
                data = JsonConvert.DeserializeObject<SizeData>(json);
            }
        }

        public string GetRandomAnswer()
        {
            if (data == null || data.Sizes == null || data.Sizes.Count == 0)
                return "No data bro";
            int index = _random.Next(data.Sizes.Count);
            return data.Sizes[index];
        }

        public class SizeData
        {
            public List<string>? Sizes { get; set; }
        }
    }
}
