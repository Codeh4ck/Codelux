using System;
using ServiceStack;

namespace Codelux.ServiceStack.IoC
{
    public interface IDependencyModule : IDisposable
    {
        ServiceStackHost AppHost { get; }
        void RegisterDependencies();
    }
}