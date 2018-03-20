﻿using Autofac;
using System.Web.Http;
using System.Configuration;
using System.Reflection;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using AlexTrebot;
using System;

namespace SimpleEchoBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Bot Storage: This is a great spot to register the private state storage for your bot. 
            // We provide adapters for Azure Table, CosmosDb, SQL Azure, or you can implement your own!
            // For samples and documentation, see: https://github.com/Microsoft/BotBuilder-Azure

            Conversation.UpdateContainer(
                builder =>
                {                    

                    // Using Azure Table Storage                    
                    string AzureConnectionString = ConfigurationManager.AppSettings["AzureWebJobsStorage"];
                    if (!String.IsNullOrEmpty(AzureConnectionString))
                    {
                        builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));

                        var store = new TableBotDataStore(AzureConnectionString); // requires Microsoft.BotBuilder.Azure Nuget package 

                        // To use CosmosDb or InMemory storage instead of the default table storage, uncomment the corresponding line below
                        // var store = new DocumentDbBotDataStore("cosmos db uri", "cosmos db key"); // requires Microsoft.BotBuilder.Azure Nuget package 
                        // var store = new InMemoryDataStore(); // volatile in-memory store

                        builder.Register(c => store)
                            .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                            .AsSelf()
                            .SingleInstance();
                    }

                });
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
