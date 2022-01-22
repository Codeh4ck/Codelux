using System;

namespace Codelux.Factories
{
    public sealed class ActivatorInstanceFactory : ITypeInstanceFactory
    {
        public TInstance CreateInstance<TInstance>(Type type)
        {
            return (TInstance)Activator.CreateInstance(type);
        }

        public TInstance CreateInstance<TInstance, TConstraint>(Type type) where TInstance : TConstraint
        {
            return CreateInstance<TInstance>(type);
        }
    }
}
