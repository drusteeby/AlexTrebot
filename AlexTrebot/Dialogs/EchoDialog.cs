using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;


namespace AlexTrebot.Dialogs
{
    [Serializable]
    public class EchoDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }


        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;           

            //The message.text could easily be printed, parsed, or anything else you wanted to do here.
            //as an example, we are passing the message.text to the callback function in RootDialog.
            await context.PostAsync($"You said: ");

            context.Done(message.Text);
        }

    }
}