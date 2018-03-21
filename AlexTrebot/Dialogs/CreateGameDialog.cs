using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using AlexTrebot.Models;

namespace AlexTrebot.Dialogs
{
    [Serializable]
    public class CreateGameDialog : IDialog<Game>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var activity = await argument;
            var arguments = activity.Text.Split(' ');

            if (arguments.Count() == 1)
            {
                StartGame(context, GetParameters());
            }
            else if (arguments.Count() == 5)
            {
                StartGame(context, arguments);
            }
            else 
            {
                context.Done((Game)null);
            }
        }

        private void StartGame(IDialogContext context, IEnumerable<string> arguments)
        {
            context.Done(new Game("test category", DateTime.Now, DateTime.Now.AddDays(1)));
        }

        private IEnumerable<string> GetParameters()
        {
            return new List<string>();
        }
    }
}