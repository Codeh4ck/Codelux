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
            if (model == null) return false;
            if (model.Roles == null || model.Roles.Count == 0) return false;

            IRole highestRole = model.Roles.MaxBy(x => x.Level);
            if (highestRole == null) return false;

            return highestRole.Level >= roleLevel;
        }

        public abstract Task<IHasRole> GetModelAsync(object key, CancellationChangeToken token = default);
    }
}
