using System.Collections.Generic;

namespace Codelux.Common.Models
{
    public interface IHasRole
    {
        public List<IRole> Roles { get; set; }
    }
}
