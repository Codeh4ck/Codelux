using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Codelux.Common.Requests;
using Codelux.Common.Responses;
using ServiceStack;

namespace Codelux.ServiceStack
{
    public sealed class CoreService : Service
    {
        public Task<VersionResponse> Get(VersionRequest request)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return Task.FromResult(new VersionResponse()
            {
                Version = versionInfo
            });
        }
    }
}
