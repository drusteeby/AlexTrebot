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
    public class HelpDialog : IDialog<string>
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
                context.Done(PrintAllHelp());
            }
            else if (arguments.Count() > 2) 
            {
                context.Done("WTF are you saying? Just say help dude...");
            }
            else
            {
                switch (arguments[1])
                {
                    case "create":
                        context.Done(PrintCreateHelp());
                        break;
                    default:
                        await context.PostAsync("C'mon man... That means nothing to me.");
                        context.Done(PrintAllHelp());
                        break;
                }
            }
        }

        private string PrintAllHelp()
        {
            return
                "This is all I can do. Don't judge me...<br/>" +
                "\t`create`:<br/>" +
                "\t\tCreate a new trivia game!<br/>" +
                "\t`answer`:<br/>" +
                "\t\tDru hasn't done this yet...<br/>" +
                "\t`viewResponses`:<br/>" +
                "\t\tNot Implemented<br/>" +
                "\t`help`:<br/>" +
                "\t\tUhh... That's this... What more do you want?<br/><br/>" +
                "For more detailed descriptions, type `help {action}`";
        }

        private string PrintCreateHelp()
        {
            return
                "Oh you wanna create a game? Alright, do something like this<br/>" +
                "\t`create`:<br/>" +
                "\t\tCreate a game by following all prompts<br/>" +
                "\t`create {category} {numQuestions} {startTime} {endTime}`<br/>" +
                "\t\tCreate a game like a boss and stop talking to me quicker";
        }
    }
}