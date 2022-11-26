using System;

namespace Codelux.Factories
{
    public sealed class ActivatorInstanceFactory : ITypeInstanceFactory
    {
        public TInstance CreateInstance<TInstance>(Type type) => (TInstance)Activator.CreateInstance(type);

        public TInstance CreateInstance<TInstance, TConstraint>(Type type) where TInstance : TConstraint => CreateInstance<TInstance>(type);
    }
}
