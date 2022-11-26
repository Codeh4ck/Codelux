using System.Collections.Generic;

namespace Codelux.Mappers
{
    public interface ISyncMapper<TIn, TOut> : IMapper<TIn, TOut>
    {
        TOut Map(TIn model);
        IEnumerable<TOut> Map(IEnumerable<TIn> models);
    }
}
