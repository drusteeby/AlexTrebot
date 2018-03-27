using System;
using System.Threading.Tasks;
using AlexTrebot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace AlexTrebot.Dialogs
{
    [Serializable]
    public class AddQuestionDialog : IDialog<Question>
    {
        private int _argIndex = 0;
        private int _number;
        private string _prompt, _answer;

        public AddQuestionDialog(int questionNumber)
        {
            _number = questionNumber;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        { 
            var activity = await argument;

            switch (_argIndex)
            {
                case 0:
                    _argIndex++;
                    await context.PostAsync("What's the question prompt?");
                    context.Wait(this.MessageReceivedAsync);
                    break;
                case 1:
                    _argIndex++;
                    _prompt = activity.Text;
                    await context.PostAsync("What's the proposed answer? (For reference when grading)");
                    context.Wait(this.MessageReceivedAsync);
                    break;
                case 2:
                    _argIndex++;
                    _answer = activity.Text;
                    context.Done(new Question(_number, _prompt, _answer));
                    break;
                default:
                    context.Done((Question)null);
                    break;
            }
        }
    }
}
