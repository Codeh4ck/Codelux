using System.Collections.Generic;

namespace Codelux.Mappers
{
    public abstract class MapperBase<TIn, TOut> : IMapper<TIn, TOut>
    {
        public abstract TOut Map(TIn model);

        public IEnumerable<TOut> Map(IEnumerable<TIn> models)
        {
            foreach (TIn model in models)
            {
                if (model == null) continue;
                yield return Map(model);
            }
        }
    }
}
