using System;
namespace AlexTrebot.Models
{
    public class Question
    {
        public string Prompt;
        public string Answer;

        public Question(string prompt, string answer)
        {
            Prompt = prompt;
            Answer = answer;
        }
    }
}
