// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using AlexTrebot.Slack.Messages;
//
//    var slackButtonMessage = SlackButtonMessage.FromJson(jsonString);

namespace AlexTrebot.Slack.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SlackButtonMessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        private SlackButtonMessage()
        {

        }

        public static SlackButtonMessage GetSlackButtonMessage()
        {
            return new SlackButtonMessage();
        }
        public static SlackButtonMessage GetExampleSlackButtonMessage()
        {

            var message = new SlackButtonMessage();
            message.Text = "Would you like to play a game?";
            message.Attachments =
                new List<Attachment>
                {
                    new Attachment
                    {
                        Text = "Choose a game to play",
                        Fallback = "You are unable to choose!",
                        CallbackId = "wopr_game",
                        Color = "#3AA3E3",
                        AttachmentType = "default",
                        Actions = new List<Action>
                        {
                            new Action { Name = "game",Text = "Chess",Type = "button",Value = "Chess"},
                            new Action { Name = "game",Text = "Falken's Maze",Type = "button",Value = "maze"},
                            new Action { Name = "game",Text = "Thermonuclear War",Style = "danger",Type = "button",Value = "war",Confirm = new Confirm{Title = "Are you sure?", Text = "Wouldn't you prefer a good game of chess?",OkText = "PULL THE TRIGGER!",DismissText = "On Second Thought..."} }
                        }
                    }
                };

            return message;
        }
    }

    public partial class Attachment
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("callback_id")]
        public string CallbackId { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("attachment_type")]
        public string AttachmentType { get; set; }

        [JsonProperty("actions")]
        public List<Action> Actions { get; set; } = new List<Action>();
    }

    public partial class Action
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("confirm")]
        public Confirm Confirm { get; set; }
    }

    public partial class Confirm
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("ok_text")]
        public string OkText { get; set; }

        [JsonProperty("dismiss_text")]
        public string DismissText { get; set; }
    }

    public partial class SlackButtonMessage
    {
        public static SlackButtonMessage FromJson(string json) => JsonConvert.DeserializeObject<SlackButtonMessage>(json, AlexTrebot.Slack.Messages.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SlackButtonMessage self) => JsonConvert.SerializeObject(self, AlexTrebot.Slack.Messages.Converter.Settings);
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                },
            },
        };
    }
}
