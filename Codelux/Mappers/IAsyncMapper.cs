using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Codelux.Mappers
{
    public interface IAsyncMapper<in TIn, TOut> : IMapper
    {
        Task<TOut> MapAsync(TIn model, CancellationToken token = default);
        IAsyncEnumerable<TOut> MapAsync(IEnumerable<TIn> models, CancellationToken token = default);
    }
}
