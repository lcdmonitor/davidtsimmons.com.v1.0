using Microsoft.Practices.Unity;
using System;
using System.Linq;
using DavidSimmons.Core.Dependency;
using System.Reflection;
using System.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace DavidSimmons.Core.Cloud
{
    public abstract class InjectableRoleEntryPoint : RoleEntryPoint, IDisposable
    {
        #region Unity Container
        private static Lazy<UnityContainer> container = new Lazy<UnityContainer>(() =>
        {
            var container = ConstructContainer();
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static UnityContainer GetCurrentContainer()
        {
            return container.Value;
        }
        #endregion 

        public override bool OnStart()
        {
            //todo: update comments Provide for Hooks for ContainerConfiguationModule
            PerformRegistrations();

            GetCurrentContainer().BuildUp(this.GetType(), this);

            return base.OnStart();
        }

        private static UnityContainer ConstructContainer()
        {
            var container = new UnityContainer();

            //TODO: GOT A LITTLE MORE TO DO HERE
            //FIND ME: this.Context.Policies.SetDefault<IPropertySelectorPolicy>(new ResolvePropertySelector());
            //Borrow Me: ResolvePropertySelector


            return container;
        }

        private void PerformRegistrations()
        {
            try
            {
                var instances = from a in AppDomain.CurrentDomain.GetAssemblies()
                                from t in a.GetTypes()
                                where t.GetInterfaces().Contains(typeof(IDependencyRegistrar))
                                && t.GetConstructor(Type.EmptyTypes) != null
                                select Activator.CreateInstance(t) as IDependencyRegistrar;

                foreach (var instance in instances)
                {
                    //TODO ABSTRACT UNITY SO I DON'T HAVE TO ADD UNITY REFERENCES EVERYWHERE
                    instance.Configure(GetCurrentContainer());
                }
            }
            catch(ReflectionTypeLoadException tle)
            {
                //todo: this is kind of a bs hack
                Debug.WriteLine(tle.ToString());
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~InjectableRoleEntryPoint() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
