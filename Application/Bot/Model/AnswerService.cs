using discord_bot.Bot.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Bot.Model
{
    public class AnswerService
    {
        private readonly Random _random;
        private readonly string _answersPath;
        public AnswerData? data;
       

        public AnswerService() 
        { 
            _random = new Random();
            _answersPath = Path.Combine(Util.GetDataPath(), "answers.json");
            data = new AnswerData();
            LoadAnswers();
        }

        private void LoadAnswers()
        {
            if (data?.Answers == null) 
            {
                var json = File.ReadAllText(_answersPath);
                data = JsonConvert.DeserializeObject<AnswerData>(json);
            }
        }

        public string GetRandomAnswer()
        {
            if (data == null  || data.Answers == null || data.Answers.Count == 0)
                return "No data bro";
            int index = _random.Next(data.Answers.Count);
            return data.Answers[index];
        }

        public class AnswerData
        {
            public List<string>? Answers { get; set; }
        }
    }
}
