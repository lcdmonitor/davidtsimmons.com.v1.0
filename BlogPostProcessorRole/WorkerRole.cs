using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.Owin.Hosting;
using DavidSimmons.Core.Cloud;
using DavidSimmons.Repository.Interfaces;
using Microsoft.Practices.Unity;
using DavidSimmons.Core.Cloud.Interfaces;
using Rebus;
using Microsoft.Azure;

namespace BlogPostProcessorRole
{
    public class WorkerRole : InjectableRoleEntryPoint
    {
        //Dependencies

        //[Resolve("Raw Logging Repository")]
        [Dependency]
        public IRawLoggingRepository rawLoggingRepository { get; set; }

        [Dependency]
        public IBusFactory busFactory { get; set; }
        //End Dependencies

        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        private IBus _bus;

        private IDisposable webApp = null;
        
        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            try
            {
                // Set the maximum number of concurrent connections 
                ServicePointManager.DefaultConnectionLimit = 12;
                
                #region SignalR Statups - todo: refactor me
                /*SignalR Statup*/

                string signalRHostPort = CloudConfigurationManager.GetSetting("SignalRHostPort");

                string signalRHostName = "+";

                string signalRHostUrl = string.Format("http://{0}:{1}", signalRHostName, signalRHostPort);

                webApp = WebApp.Start<Startup>(signalRHostUrl);

                Trace.WriteLine(string.Format("SignalR Server running at {0}/SignalR/Hubs", signalRHostUrl));

                /*SignalR Statup*/
                #endregion

                //todo: this is a little bogus maybe shouuld catch the return value or something... coudl be an inheritance problem
                base.OnStart();

                #region Bootup The Bus
                //TODO: Breakup the Processing of Visits into a separate Worker Role, needs a separate queue.
                _bus = busFactory.CreateBus(GetCurrentContainer());
                #endregion

                return true;

            }
            catch (Exception e)
            {
                //TODO: REMOVE SECOND PARAMETER AND RESOLVE ID WITH LOG METHOD
                rawLoggingRepository.CreateLogEntryForException(e, RoleEnvironment.CurrentRoleInstance.Id);

                return false;
            }
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            CompletedEvent.Set();

            #region teardown the bus
            _bus.Dispose();
            #endregion

            webApp.Dispose();

            base.OnStop();
        }
    }
}
