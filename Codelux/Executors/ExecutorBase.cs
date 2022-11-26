using System.Threading;
using System.Threading.Tasks;

namespace Codelux.Executors
{
    public abstract class ExecutorBase<TIn, TOut> : IExecutor<TIn, TOut>
    {
        public async Task<TOut> ExecuteAsync(TIn tin, CancellationToken token = new()) => await OnExecuteAsync(tin, token).ConfigureAwait(false);
        protected abstract Task<TOut> OnExecuteAsync(TIn tin, CancellationToken token = default);
    }
}
