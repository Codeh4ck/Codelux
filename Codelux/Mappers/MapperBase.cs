using System.Collections.Generic;
using System.Linq;

namespace Codelux.Mappers
{
    public abstract class MapperBase<TIn, TOut> : IMapper<TIn, TOut>
    {
        public abstract TOut Map(TIn model);

        public IEnumerable<TOut> Map(IEnumerable<TIn> models) => from model in models where model != null select Map(model);
    }
}
