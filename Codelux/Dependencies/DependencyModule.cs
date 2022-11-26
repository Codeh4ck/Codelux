using System;
using Unity;

namespace Codelux.Dependencies
{
    public abstract class DependencyModule : IDependencyModule
    {
        private readonly IUnityContainer _container;
        protected DependencyModule(IUnityContainer container) => _container = container ?? throw new ArgumentNullException(nameof(container));

        public void RegisterDependencies() => OnRegisterDependencies(_container);

        public abstract void OnRegisterDependencies(IUnityContainer container);
    }
}
