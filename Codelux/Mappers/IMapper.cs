using System.Collections.Generic;

namespace Codelux.Mappers
{
    public interface IMapper<in TIn, out TOut>
    {
        TOut Map(TIn model);
        IEnumerable<TOut> Map(IEnumerable<TIn> models);
    }
}
