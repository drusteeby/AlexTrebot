using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexTrebot.Slack
{
    public class SlackMessageMapper : IMessageActivityMapper
    {
        public IMessageActivity Map(IMessageActivity message)
        {
            //if (message.ChannelData is MyCustomReceipt)
            //{
            //    var customReceipt = (MyCustomReceipt)message.ChannelData;
            //    message.ChannelData = magicCodeThatGeneratesFacebookSpecificPayload(customReceipt);
            //}
            //else { /* handle other payload types */ }

            return message;
        }
    }
}