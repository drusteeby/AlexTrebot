using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using System.Threading;

namespace AlexTrebot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        //This is the "entry" point for all messages recieved by this bot. 
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var lowercasemessage = message.Text.ToLower();

            //Decide what dialog to call based on the contents of the first message
            if (lowercasemessage.Contains("creategame"))
            {
                await context.PostAsync($"Add the new game dialog here!");
                // call a new dialog here e.g.:
                // await context.Forward(new NewOrderDialog(), this.ResumeAfterNewOrderDialog, message, CancellationToken.None);
            }
            else if (lowercasemessage.Contains("answer"))
            {
                await context.PostAsync($"Add the answer dialog here!");
            }
            else if (lowercasemessage.Contains("help"))
            {
                await context.PostAsync($"Add the help dialog here!");
            }
            else if (lowercasemessage.Contains("nahkyledoes"))
            {
                await context.PostAsync($"you right, also, Github works again!");
            }
            //else if(lowercasemessage.Contains("echo"))
            //{
            //    context.Call(new EchoDialog(message), ResumeAfterEchoDialog);
            //}
            else
            {
                await context.PostAsync($"That is not a command I recognize! Possible commands are creategame, answer, viewResponses...etc..we're still working on it");
                context.Wait(this.MessageReceivedAsync);
            }
        }

        //e.g of a "Resume After dialog" function.
        //private async Task ResumeAfterEchoDialog(IDialogContext context, IAwaitable<string> result)
        //{
        //    // Store the value that NewOrderDialog returned. 
        //    // (At this point, new order dialog has finished and returned some value to use within the root dialog.)
        //    string resultFromEchoDialog = await result;

        //    await context.PostAsync(resultFromEchoDialog);

        //    // Again, wait for the next message from the user.
        //    context.Wait(this.MessageReceivedAsync);
        //}
    }
}