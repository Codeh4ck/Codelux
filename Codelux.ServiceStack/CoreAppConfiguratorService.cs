using System;
using Codelux.ServiceStack.Plugins;
using ServiceStack;

namespace Codelux.ServiceStack
{
    public abstract class CoreAppConfiguratorBase : IDisposable
    {
        private readonly string _serviceName;

        protected CoreAppConfiguratorBase(string serviceName, ServiceStackHost appHost)
        {
            if (appHost == null) throw new ArgumentNullException(nameof(appHost));
            if (serviceName == null) throw new ArgumentNullException(nameof(serviceName));

            _serviceName = serviceName;

            appHost.Plugins.Add(new CoreServiceFeature());
            appHost.ServiceExceptionHandlers.Add(new ServiceErrorExceptionHandler(serviceName).Handle);
        }

        public virtual void Dispose() { }
    }
}
