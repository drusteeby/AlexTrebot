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

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        //This is the "entry" point for all messages recieved by this bot. 
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var lowercasemessage = message.Text.ToLower();

            //Decide what dialog to call based on the contents of the first message
            if (lowercasemessage.Contains("create"))
            {
                await context.Forward(new CreateGameDialog(lowercasemessage), 
                                      this.ResumeAfterDialog, 
                                      message, 
                                      CancellationToken.None);
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
            else if (lowercasemessage.Contains("echo"))
            {
               await context.Forward(new EchoDialog(), ResumeAfterEchoDialog,message,CancellationToken.None);
            }
            else if (lowercasemessage.Contains("printchanneldata"))
            {                
                await context.PostAsync($"Channel Data: {message.ChannelData.ToString()}");
            }
            else
            {
                await context.PostAsync(
                    $"That is not a command I recognize! Possible commands are " +
                    "create, answer, viewResponses...etc..we're still working on it");
                context.Wait(this.MessageReceivedAsync);
            }
        }

        //e.g of a "Resume After dialog" function.
        private async Task ResumeAfterEchoDialog(IDialogContext context, IAwaitable<string> result)
        {
            // Store the value that dialog returned. 
            // At this point the dialog has completed and we can parse the result
            //e.g. A "Create Game" Dialog might return a complete "Game" object, which we would store in the database
            //Here, echo dialog is returning the same message that the bot received. 
            string resultFromEchoDialog = await result;

            //Echo the message back to the user
            await context.PostAsync(resultFromEchoDialog);

            //Wait for the next message from the user
            context.Wait(this.MessageReceivedAsync);
        }
    }
}