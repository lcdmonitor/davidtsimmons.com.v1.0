using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;

namespace DavidSimmons.Hubs
{
    public class HubAssemblyLocator : IAssemblyLocator
    {
        public IList<System.Reflection.Assembly> GetAssemblies()
        {
            // list your hubclass assemblies here
            return new List<System.Reflection.Assembly>(new[] { this.GetType().Assembly });
        }
    }
}