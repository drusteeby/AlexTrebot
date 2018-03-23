using System;
using System.Collections.Generic;

namespace AlexTrebot.Models
{
    [Serializable]
    public class Game
    {
        public string Category;
        public IEnumerable<Question> Questions;
        public DateTime StartTime;
        public DateTime EndTime;

        public Game(string category, IEnumerable<Question> questions, DateTime startTime, DateTime endTime)
        {
            Category = category;
            Questions = questions;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString(){
            return
                $"Category: {Category}, {StartTime}-{EndTime}<br/>" +
                string.Join("<br/>", Questions);
        }
    }
}
