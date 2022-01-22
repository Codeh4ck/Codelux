using Codelux.Plugins.Metadata;

namespace Codelux.Plugins.Base
{
    public interface IPluginConfigurationSource
    {
        List<PluginConfiguration> ReadConfiguration(bool readFromSource = true);
        void WriteConfiguration();
        bool TryAddConfiguration(PluginConfiguration configuration);
        bool RemoveConfiguration(Type type);
    }
}
