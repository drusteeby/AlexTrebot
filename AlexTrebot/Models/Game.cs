using System;
using System.Collections.Generic;

namespace AlexTrebot.Models
{
    public class Game
    {
        public string Category;
        public List<Question> Questions;
        public DateTime StartTime;
        public DateTime EndTime;

        public Game(string category, DateTime startTime, DateTime endTime)
        {
            Category = category;
            Questions = new List<Question>();
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
