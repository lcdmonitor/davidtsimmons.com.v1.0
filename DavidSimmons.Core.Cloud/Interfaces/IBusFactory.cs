using Microsoft.Practices.Unity;
using Rebus;

namespace DavidSimmons.Core.Cloud.Interfaces
{
    public interface IBusFactory
    {
        IBus CreateBus(UnityContainer container, bool sendOnly = false);
    }
}
