using Microsoft.Practices.Unity;

namespace DavidSimmons.Core.Dependency
{
    public interface IDependencyRegistrar
    {
        void Configure(UnityContainer container);
    }
}
