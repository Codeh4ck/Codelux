using Codelux.Plugins.Metadata;
using Codelux.Runnables;

namespace Codelux.Plugins.Base
{
    public interface IPluginInstanceManager<TPlugin> : IRunnable where TPlugin : IPlugin
    {
        void ProcessPlugins(Action<TPlugin> process);
        Task ProcessPluginsAsync(Action<TPlugin> process, CancellationToken token = default);
        List<PluginConfiguration> GetPluginConfigurations();
        List<TPlugin> GetRunningPlugins();
        List<TPlugin> GetLoadedPlugins();
        void UnloadPlugin(Guid pluginId);
    }
}
