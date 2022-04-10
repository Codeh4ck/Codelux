using System;
using Codelux.Common.Models;

namespace Codelux.ServiceStack.Roles
{
    public interface IProtectedRouteCollection
    {
        bool AddProtectedRoute(Type requestType, IRole role);
        bool RemoveProtectedRoute(Type requestType);
        bool IsProtectedRequestType(Type requestType);
        bool CanExecute(Type requestType, IHasRole role);
    }
}
