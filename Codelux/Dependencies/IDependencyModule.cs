using Unity;

namespace Codelux.Dependencies
{
    public interface IDependencyModule
    {
        void OnRegisterDependencies(IUnityContainer unityContainer);
    }
}
