using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Codelux.Dependencies
{
    public sealed class DependencyRegistrar : UnityContainer
    {
        private bool _dependenciesRegistered;

        public void RegisterAllDependencies()
        {
            if (_dependenciesRegistered) throw new InvalidOperationException("Dependencies have already been registered.");

            Type baseType = typeof(IDependencyModule);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                List<Type> types = assembly.GetTypes().Where(type => type != baseType && !type.IsAbstract && baseType.IsAssignableFrom(type)).ToList();

                foreach (Type type in types)
                {
                    IDependencyModule instance = (IDependencyModule)Activator.CreateInstance(type, this);
                    instance.OnRegisterDependencies(this);
                }
            }

            _dependenciesRegistered = true;
        }
    }
}
