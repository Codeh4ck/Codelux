using ServiceStack;

namespace Codelux.ServiceStack.IoC
{
    public abstract class DependencyModuleBase : IDependencyModule
    {
        public virtual ServiceStackHost AppHost { get; }

        protected DependencyModuleBase(ServiceStackHost appHost) => AppHost = appHost;

        public virtual void Dispose() { }
        public abstract void RegisterDependencies();
    }
}