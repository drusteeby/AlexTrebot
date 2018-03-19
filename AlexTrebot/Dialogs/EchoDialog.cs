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
        string[] commandParams;
        protected int count = 0;
        private BotData userData;
        private StateClient stateClient;

        public EchoDialog(IMessageActivity message)
        {
            this.message = message;

            //get the "params" from the "echo" command by splitting on whitespace
            commandParams = message.Text.Split(null);

            //Load the count from User Data            
            //stateClient = message.GetStateClient();
            //userData =  stateClient.BotState.GetUserData(message.ChannelId, message.From.Id); //TODO: This method is depreciated
            //count = userData.GetProperty<int>("Count");
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (commandParams.Length == 1)
            {
                //Prompt here for more information
            }
            else if (commandParams.Length == 2)
            {
                if (commandParams[1] == "reset")
                {
                    PromptDialog.Confirm(
                        context,
                        AfterResetAsync,
                        "Are you sure you want to reset the count?",
                        "Didn't get that!",
                        promptStyle: PromptStyle.Auto);
                }
            }

            await SaveUserData();

            context.Done($"{this.count++}: You said {message.Text}");
        }

        private async Task SaveUserData()
        {
            //userData.SetProperty("Count", count);
            //await stateClient.BotState.SetUserDataAsync(message.ChannelId, message.From.Id, userData);
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

            await SaveUserData();
            context.Done($"{this.count++}: You said {message.Text}");
        }

    }
}