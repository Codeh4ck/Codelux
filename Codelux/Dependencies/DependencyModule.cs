using System;
using Unity;

namespace Codelux.Dependencies
{
    public abstract class DependencyModule : IDependencyModule
    {
        private readonly IUnityContainer _container;
        protected DependencyModule(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            _container = container;
        }

        public void RegisterDependencies()
        {
            OnRegisterDependencies(_container);
        }

        public abstract void OnRegisterDependencies(IUnityContainer container);
    }
}
