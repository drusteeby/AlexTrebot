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
            if(lowercasemessage.Contains("creategame"))
            {
                await context.PostAsync($"Add the new game dialog here!");
                // call a new dialog here 
                // await context.Forward(new NewOrderDialog(), this.ResumeAfterNewOrderDialog, message, CancellationToken.None);
            }
            else if(lowercasemessage.Contains("answer"))
            {
                await context.PostAsync($"Add the answer dialog here!");
            }
            else if (lowercasemessage.Contains("help"))
            {
                await context.PostAsync($"Add the help dialog here!");
            }
            else if(lowercasemessage.Contains("echo"))
            {
                await context.PostAsync($"Add the echo dialog here!");
                //await context.Forward(new EchoDialog(message), this.ResumeAfterEchoDialog, message, CancellationToken.None);
            }
            else
            {
                await context.PostAsync($"That is not a command I recognize! Possible commands are creategame, answer, viewResponses...etc..we're still working on it");
                context.Wait(this.MessageReceivedAsync);
            }            
        }               

    }
}