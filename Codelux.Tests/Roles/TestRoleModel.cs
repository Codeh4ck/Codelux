using System;
using System.Collections.Generic;
using Codelux.Common.Models;

namespace Codelux.Tests.Roles
{
    public class TestRoleModel : IHasRole
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public List<IRole> Roles { get; set; }
    }
}
