using Codelux.Common.Requests;
using ServiceStack;

namespace Codelux.ServiceStack.Plugins
{
    public class CoreServiceFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.Routes.Add<VersionRequest>("/api/version", "Get");
            appHost.RegisterService<CoreService>();
        }
    }
}
