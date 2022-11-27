using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Codelux.Mappers
{
    public abstract class AsyncMapperBase<TIn, TOut> : IAsyncMapper<TIn, TOut>
    {
        public abstract Task<TOut> MapAsync(TIn model, CancellationToken token = default);

        public async IAsyncEnumerable<TOut> MapAsync(IEnumerable<TIn> models, [EnumeratorCancellation] CancellationToken token = default)
        {
            foreach (TIn model in models)
                yield return await MapAsync(model, token).ConfigureAwait(false);
        }
        public async Task<List<TOut>> MapToListAsync(IEnumerable<TIn> models, CancellationToken token = default)
        {
            IAsyncEnumerable<TOut> mappedModels = MapAsync(models, token);
            return await mappedModels.ToListAsync(token).ConfigureAwait(false);
        }
    }
}
