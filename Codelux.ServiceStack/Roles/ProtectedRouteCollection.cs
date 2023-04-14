using System;
using System.Collections.Concurrent;
using Codelux.Common.Extensions;
using Codelux.Common.Models;

namespace Codelux.ServiceStack.Roles
{
    public class ProtectedRouteCollection : IProtectedRouteCollection
    {
        private readonly ConcurrentDictionary<Type, IRole> _protectedRoutes;
        private readonly IRoleValidator _roleValidator;

        public ProtectedRouteCollection(IRoleValidator roleValidator)
        {
            roleValidator.Guard(nameof(roleValidator));
            _roleValidator = roleValidator;

            _protectedRoutes = new();
        } 

        public bool AddProtectedRoute(Type requestType, IRole role) => _protectedRoutes.TryAdd(requestType, role);
        public bool RemoveProtectedRoute(Type requestType) => _protectedRoutes.TryRemove(requestType, out _);
        public bool IsProtectedRequestType(Type requestType) => _protectedRoutes.ContainsKey(requestType);

        public bool CanExecute(Type requestType, IHasRole model)
        {
            bool exists = _protectedRoutes.TryGetValue(requestType, out IRole requiredRole);
            if (!exists) return false;

            return _roleValidator.HasRole(model, requiredRole.Level);
        }
    }
}
