using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Codelux.Mappers
{
    public interface IAsyncMapper<TIn, TOut> : IMapper<TIn, TOut>
    {
        Task<TOut> MapAsync(TIn model, CancellationToken token = default);
        IAsyncEnumerable<TOut> MapAsync(IEnumerable<TIn> models, CancellationToken token = default);
    }
}
