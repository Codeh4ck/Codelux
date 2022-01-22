using System;
using ServiceStack;

namespace Codelux.ServiceStack.Utilities
{
    public interface IDependencyModule : IDisposable
    {
        ServiceStackHost AppHost { get; }
        void RegisterDependencies();
    }
}
