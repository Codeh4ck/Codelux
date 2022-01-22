using System;

namespace Codelux.Common.Requests
{
    public class AuthenticatedRequest : Request
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
