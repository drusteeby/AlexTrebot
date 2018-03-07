using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using AlexTrebot.Slack.Messages;

namespace AlexTrebot.Dialogs
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.ChannelData.Payload != null &&
                message.ChannelData.Payload.type != null &&
                message.ChannelData.Payload.type.Value == "interactive_message")
            {
                if (message.Text.ToLower() == "chess")
                {
                    await context.PostAsync($"Checkmate, Athiests!");
                }
                else if (message.Text.ToLower() == "maze")
                {
                    await context.PostAsync($"ERROR ERROR ERROARRRR: You totally meant to select \"Chess\"");
                }
                else if (message.Text.ToLower() == "war")
                {
                    await context.PostAsync($"Welp, you ded. You just had to pull the trigger, didn't ya?");
                }

                context.Wait(MessageReceivedAsync);
            }

            else if (message.Text == "reset")
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
                PromptDialog.Text(context, AfterTextResetAsync, "This is a test of a string input", "Sorry, I didn't hear you");
            }
            else if (message.Text == "button")
            {
                var reply = context.MakeMessage();
                reply.ChannelData = SlackButtonMessage.GetExampleSlackButtonMessage();
                await context.PostAsync(reply);

                context.Wait(MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync($"{this.count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task ButtonResponseReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Text.ToLower() == "chess")
            {
                await context.PostAsync($"Checkmate, Athiests!");
            }
            else if (message.Text.ToLower() == "maze")
            {
                await context.PostAsync($"ERROR ERROR ERROARRRR: You totally meant to select \"Chess\"");
            }
            else if (message.Text.ToLower() == "war")
            {
                await context.PostAsync($"Welp, you ded. You just had to pull the trigger, didn't ya?");
            }

            context.Wait(MessageReceivedAsync);
        }

        private async Task AfterTextResetAsync(IDialogContext context, IAwaitable<string> result)
        {
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