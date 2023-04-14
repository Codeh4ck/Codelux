using Codelux.Common.Extensions;
using Codelux.Factories;
using Codelux.Plugins.Base;
using Codelux.Plugins.Metadata;
using Codelux.Runnables;

namespace Codelux.Plugins
{
    public class PluginInstanceManager<TPlugin> : Runnable, IPluginInstanceManager<TPlugin> where TPlugin : IPlugin
    {
        private Dictionary<Guid, TPlugin> _plugins;
        private readonly List<PluginConfiguration> _pluginConfigurations;
        private readonly ITypeInstanceFactory _instanceFactory;

        public PluginInstanceManager(IPluginConfigurationSource configSource, ITypeInstanceFactory instanceFactory)
        {
            configSource.Guard(nameof(configSource));
            instanceFactory.Guard(nameof(instanceFactory));

            _pluginConfigurations = configSource.ReadConfiguration(false);

            if (_pluginConfigurations.Count == 0)
                _pluginConfigurations = configSource.ReadConfiguration();

            _instanceFactory = instanceFactory;
        }

        public void ProcessPlugins(Action<TPlugin> process)
        {
            foreach (TPlugin plugin in _plugins.Values)
                process(plugin);
        }

        public async Task ProcessPluginsAsync(Action<TPlugin> process, CancellationToken token = default)
        {
            Task[] processTasks = new Task[_plugins.Count];
            int index = 0;

            foreach (var kvp in _plugins)
            {
                TPlugin plugin = kvp.Value;
                processTasks[index] = Task.Run(() => process(plugin), token);
                index++;
            }

            await Task.WhenAll(processTasks).ConfigureAwait(false);
        }

        public List<PluginConfiguration> GetPluginConfigurations() => _pluginConfigurations;

        public List<TPlugin> GetRunningPlugins() => _plugins.Values.Where(plugin => plugin.IsRunning).ToList();

        public List<TPlugin> GetLoadedPlugins() => _plugins.Values.ToList();

        public void UnloadPlugin(Guid pluginId)
        {
            bool exists = _plugins.Remove(pluginId, out TPlugin plugin);
            if (!exists) return;

            if (plugin.IsRunning)
                plugin.Stop();
        }

        protected override void OnStart(object context = null)
        {
            _plugins = new();

            foreach (PluginConfiguration config in _pluginConfigurations)
            {
                Guid pluginId = config.Header.Id;
                Type pluginType = config.PluginType;

                TPlugin instance = _instanceFactory.CreateInstance<TPlugin>(pluginType);
                instance.Configure(config);
                instance.Start(context);

                _plugins.Add(pluginId, instance);
            }
        }

        protected override void OnStop()
        {
            foreach (TPlugin plugin in _plugins.Values)
                plugin.Stop();
        }
    }
}
