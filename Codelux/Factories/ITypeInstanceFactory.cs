using System;

namespace Codelux.Factories
{
    public interface ITypeInstanceFactory
    {
        TInstance CreateInstance<TInstance>(Type type);
        TInstance CreateInstance<TInstance, TConstraint>(Type type) where TInstance : TConstraint;
    }
}
