using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexTrebot.Channels
{
    public class DefaultMessageMapper : IMessageActivityMapper
    {
        public IMessageActivity Map(IMessageActivity message)
        {
            return message;
        }
    }
}