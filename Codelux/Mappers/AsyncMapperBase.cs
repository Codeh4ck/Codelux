using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Codelux.Mappers
{
    public abstract class AsyncMapperBase<TIn, TOut> : MapperBase<TIn, TOut>
    {
        public Task<TOut> MapAsync(TIn model, CancellationToken token = default) => Task.Factory.StartNew(() => Map(model), token);
        public Task<IEnumerable<TOut>> MapAsync(IEnumerable<TIn> models, CancellationToken token = default)
            => Task.Factory.StartNew(() => Map(models), token);
    }
}
