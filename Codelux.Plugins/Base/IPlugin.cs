using Codelux.Plugins.Metadata;
using Codelux.Runnables;

namespace Codelux.Plugins.Base
{
    public interface IPlugin : IRunnable
    {
        void Configure(PluginConfiguration configuration);
    }
}
