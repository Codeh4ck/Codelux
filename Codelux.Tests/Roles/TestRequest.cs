using Codelux.Common.Requests;

namespace Codelux.Tests.Roles
{
    public class TestRequest : AuthenticatedRequest
    {
        public string Message { get; set; }
    }
}
