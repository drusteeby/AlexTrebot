using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;

namespace AlexTrebot.Dialogs
{
    [Serializable]
    public class CreateGameDialog : IDialog<string>
    {
        private List<string> arguments;

        public CreateGameDialog(string message)
        {
            this.arguments = message.Split(' ').ToList();
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var result = await argument;

            if (arguments.Count() < 2)
            {
                context.Done($"Your input does not contain all expected arguments: {result.Text}");
            }
            else {
                context.Done($"Let's make a game! {result.Text}");
            }
        }
    }
}