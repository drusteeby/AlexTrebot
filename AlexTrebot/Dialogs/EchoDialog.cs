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
        private IMessageActivity message;
        protected int count = 0;
        private BotData userData;
        private StateClient stateClient;

        public EchoDialog(IMessageActivity message)
        {
            this.message = message;

            //Load the count from User Data            
            stateClient = message.GetStateClient();
            userData =  stateClient.BotState.GetUserData(message.ChannelId, message.From.Id); //TODO: This method is depreciated
            count = userData.GetProperty<int>("Count");
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (message.Text == "reset")
            {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "Are you sure you want to reset the count?",
                    "Didn't get that!",
                    promptStyle: PromptStyle.Auto);
            }
            else if (message.Text == "string")
            {
                PromptDialog.Text(context, AfterStringAsync, "This is a test of a string input", "Sorry, I didn't hear you");
            }
            else
            {               

               
            }

            context.Done($"{this.count++}: You said {message.Text}");
        }       
        

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            
            
            
        }

        private async Task AfterStringAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var result = await argument;
            await context.PostAsync($"Your string was: {result}");
            context.Wait(MessageReceivedAsync);
        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }

    }
}