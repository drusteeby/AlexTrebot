using AlexTrebot.Slack;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexTrebot.Channels
{
    public class ChannelMapper : IMessageActivityMapper
    {
        /// <summary>
        /// A map between channelID (key) and the transformer implementation (value).
        /// </summary>
        /// <remarks>ChannelID is defined by the bot framework. Check link below for details.</remarks>
        /// <seealso cref="https://docs.botframework.com/en-us/csharp/builder/sdkreference/channels.html#customfacebookmessages"/>
        private static readonly Dictionary<string, IMessageActivityMapper> Transformers =
          new Dictionary<string, IMessageActivityMapper>(StringComparer.OrdinalIgnoreCase)
        {
            { "slack", new SlackMessageMapper() }
              /* ... add more channels here ... */
        };

        private static readonly IMessageActivityMapper DefaultMapper = new DefaultMessageMapper();

        /// <summary>
        /// Maps a message to its channel specific format.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The transformed message.</returns>
        public IMessageActivity Map(IMessageActivity message)
        {
            var customMessage = message.ChannelData;

            if (customMessage != null)
            {
                IMessageActivityMapper transformer;
                if (!Transformers.TryGetValue(message.ChannelId, out transformer))
                {
                    transformer = DefaultMapper;
                }

                message = transformer.Map(message);
            }

            return message;
        }
    }
}