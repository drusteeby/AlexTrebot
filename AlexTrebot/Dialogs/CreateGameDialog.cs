using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using AlexTrebot.Models;
using System.Threading;

namespace AlexTrebot.Dialogs
{
    [Serializable]
    public class CreateGameDialog : IDialog<Game>
    {
        private int _argIndex = 0;
        private string _category;
        private List<Question> _questions = new List<Question>();
        private DateTime _startTime, _endTime;

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
                    await context.PostAsync("What's the category for this game of trivia?");
                    context.Wait(this.MessageReceivedAsync);
                    break;
                case 1:
                    _argIndex++;
                    _category = activity.Text;
                    await context.PostAsync("When should your trivia game start?");
                    context.Wait(this.MessageReceivedAsync);
                    break;
                case 2:
                    var tempStart = activity.Text;
                    if (!DateTime.TryParse(tempStart, out _startTime))
                    {
                        await context.PostAsync("Why don't you enter a date and time?");
                        context.Wait(this.MessageReceivedAsync);
                        break;
                    }

                    _argIndex++;
                    await context.PostAsync("When should your trivia game end?");
                    context.Wait(this.MessageReceivedAsync);
                    break;
                case 3:
                    var tempEnd = activity.Text;
                    if (!DateTime.TryParse(tempEnd, out _endTime))
                    {
                        await context.PostAsync("Why don't you enter a date and time?");
                        context.Wait(this.MessageReceivedAsync);
                        break;
                    }
                    _argIndex++;
                    await context.PostAsync("Would you like to add a question?");
                    context.Wait(this.MessageReceivedAsync);
                    break;
                case 4:
                    if (activity.Text.ToLower() == "y" || activity.Text.ToLower() == "yes")
                    {
                        await context.Forward(new AddQuestionDialog(_questions.Count() + 1),
                                      ResumeAfterAddQuestionDialog,
                                      activity,
                                      CancellationToken.None);
                        break;
                    }
                    
                    context.Done(new Game(_category, _questions, _startTime, _endTime));
                    break;
                default:
                    context.Done((Game)null);
                    break;
            }
        }

        private async Task ResumeAfterAddQuestionDialog(IDialogContext context, IAwaitable<Question> result)
        {
            var question = await result;
            _questions.Add(question);

            await context.PostAsync("Would you like to add a question?");
            context.Wait(this.MessageReceivedAsync);
        }
    }
}