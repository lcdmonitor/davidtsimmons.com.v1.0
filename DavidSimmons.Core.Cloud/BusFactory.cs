using DavidSimmons.Core.Cloud.Interfaces;
using Microsoft.Practices.Unity;
using Rebus;
using Rebus.Configuration;
using Rebus.Unity;
using Rebus.AzureServiceBus;
using Rebus.Logging;
using System;
using System.Linq;

namespace DavidSimmons.Core.Cloud
{
    public class BusFactory : IBusFactory
    {
        //TODO: MAKE ME DYNAMIC
#if DEBUG
        private static string _queuename = "ProcessingDevelopmentQueue";
        private static string _errorqueuename = "ProcessingDevelopmentQueueError";
#endif
#if !DEBUG
        private static string _queuename = "ProcessingQueue";
        private static string _errorqueuename = "ProcessingQueueError";
#endif
        public static string QueueName
        {
            get
            {
                return _queuename;
            }
        }

        public IBus CreateBus(UnityContainer container, bool sendOnly = false)
        {
            //TODO: NOT SURE IF I SHOULD BE CREATING A BUS EVERYTIME, SEEMS COSTLY, IS IT THREADSAFE?
            var adapter = new UnityContainerAdapter(container);

            var bus = Configure.With(adapter)
                .Logging(l => {
                    if(sendOnly)
                    {
                        l.ColoredConsole();
                    }
                    else
                    {
                        l.Trace();
                    }
                    l.Use(new GhettoFileLoggerFactory("c:\\log.txt"));
                }) //TODO: CHINCEY CONDITION
                //TODO: DO NOT HARDCODE STRINGS
                .Transport(t =>
                {
                    if (!sendOnly)
                    {
                        t.UseAzureServiceBus("Endpoint=sb://dsimmons.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=P6c9hLLAyMVal3pvphHJdUZU/kXrQIj19tGvtPPYjaU=", _queuename, _errorqueuename);
                    }
                    else
                    {
                        t.UseAzureServiceBusInOneWayClientMode("Endpoint=sb://dsimmons.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=P6c9hLLAyMVal3pvphHJdUZU/kXrQIj19tGvtPPYjaU=");
                    }
                })
                .MessageOwnership(d =>
                    {
                        if (sendOnly)
                        {
                            d.Use(new CustomizedEndpointMapper());
                        }
                    })
                    .CreateBus()
                    .Start(sendOnly ? 0 : 10);//TODO: make me configurable

            return bus;
        }

        class CustomizedEndpointMapper : IDetermineMessageOwnership
        {
            public CustomizedEndpointMapper()
            {

            }

            public string GetEndpointFor(Type messageType)
            {
                return BusFactory.QueueName;
            }
        }
    }
}
