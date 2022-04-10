using System;
using System.Linq;
using System.Threading.Tasks;
using Codelux.Common.Models;
using Microsoft.Extensions.Primitives;

namespace Codelux.ServiceStack.Roles
{
    public abstract class RoleValidatorBase : IRoleValidator
    {
        public bool HasRole(IHasRole model, int roleLevel)
        {
            if (model.Roles == null || model.Roles.Count == 0) return false;

            IRole highestRole = model.Roles.OrderByDescending(x => x.Level).FirstOrDefault();
            if (highestRole == null) return false;

            return highestRole.Level >= roleLevel;
        }

        public abstract Task<IHasRole> GetModelAsync(object key, CancellationChangeToken token = default);
    }
}
