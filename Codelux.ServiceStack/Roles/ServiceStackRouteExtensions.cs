using System;
using ServiceStack;
using System.Collections.Generic;
using Codelux.Common.Models;
using ServiceStack.Web;

namespace Codelux.ServiceStack.Roles
{
    public static class ServiceStackRouteExtensions
    {
        public static void AddProtectedRoute<TRequest>(this IAppHost appHost, string restPath, IRole minimumRole, string verbs)
        {
            IProtectedRouteCollection protectedRouteCollection = appHost.GetContainer().Resolve<IProtectedRouteCollection>();

            if (protectedRouteCollection == null)
                throw new NotImplementedException("Protected route collection is not registered in the IoC.");

            if (!protectedRouteCollection.AddProtectedRoute(typeof(TRequest), minimumRole))
                throw new InvalidOperationException("Could not register route to the protected route collection.");
  
            appHost.Routes.Add<TRequest>(restPath, verbs);
        }

        public static void AddProtectedRoute<TRequest>(this IAppHost appHost, string restPath, IRole minimumRole, ApplyTo verbs) 
            => appHost.AddProtectedRoute<TRequest>(restPath, minimumRole, verbs.ToVerbsString());

        private static string ToVerbsString(this ApplyTo verbs)
        {
            List<string> allowedMethods = new();

            foreach (var entry in ApplyToUtils.ApplyToVerbs)
            {
                if (verbs.Has(entry.Key))
                    allowedMethods.Add(entry.Value);
            }

            return string.Join(" ", allowedMethods.ToArray());
        }
    }
}
