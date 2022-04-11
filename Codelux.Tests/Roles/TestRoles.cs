using Codelux.Common.Models;

namespace Codelux.Tests.Roles
{
    public class MemberRole : IRole
    {
        public int Id { get; set; } = 0;
        public int Level { get; set; } = 0;
        public string Name { get; set; } = "Member";
    }

    public class ModeratorRole : IRole
    {
        public int Id { get; set; } = 1;
        public int Level { get; set; } = 1;
        public string Name { get; set; } = "Moderator";
    }

    public class AdminRole : IRole
    {
        public int Id { get; set; } = 2;
        public int Level { get; set; } = 2;
        public string Name { get; set; } = "Administrator";
    }
}
