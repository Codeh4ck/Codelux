using Codelux.Plugins.Metadata;
using Codelux.Runnables;

namespace Codelux.Plugins.Base
{
    public abstract class PluginBase : Runnable, IPlugin
    {
        public PluginConfiguration Configuration { get; set; }

        protected PluginBase() { }

        public abstract void Configure(PluginConfiguration configuration);
    }
}
