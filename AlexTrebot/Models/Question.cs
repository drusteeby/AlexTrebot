using System;
namespace AlexTrebot.Models
{
    [Serializable]
    public class Question
    {
        public int Number;
        public string Prompt;
        public string Answer;

        public Question(int number, string prompt, string answer)
        {
            Number = number;
            Prompt = prompt;
            Answer = answer;
        }

        public override string ToString()
        {
            return
                $"Q{Number}: {Prompt}, A{Number}: {Answer}";
        }
    }
}
