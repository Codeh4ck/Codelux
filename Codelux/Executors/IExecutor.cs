using System.Threading;
using System.Threading.Tasks;

namespace Codelux.Executors
{
    public interface IExecutor<in TIn, TOut>
    {
        Task<TOut> ExecuteAsync(TIn tin, CancellationToken token = default);
    }
}
