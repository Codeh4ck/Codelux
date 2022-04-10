using System.Threading.Tasks;
using Codelux.Common.Models;
using Microsoft.Extensions.Primitives;

namespace Codelux.ServiceStack.Roles
{
    public interface IRoleValidator
    {
        bool HasRole(IHasRole model, int roleLevel);
        Task<IHasRole> GetModelAsync(object key, CancellationChangeToken token = default);
    }
}
