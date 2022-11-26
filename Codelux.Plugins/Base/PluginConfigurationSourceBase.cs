using Codelux.Common.Extensions;
using Codelux.Plugins.Metadata;

namespace Codelux.Plugins.Base
{
    public abstract class PluginConfigurationSourceBase : IPluginConfigurationSource
    {
        protected readonly string BaseDirectory;
        protected readonly Dictionary<Type, PluginConfiguration> PluginConfigurations;

        protected PluginConfigurationSourceBase(string baseDirectory, bool createIfNotExists = false)
        {
            baseDirectory.Guard();

            if (!Directory.Exists(baseDirectory))
            {
                if (createIfNotExists)
                    // ReSharper disable once AssignNullToNotNullAttribute
                    Directory.CreateDirectory(baseDirectory);
                else
                    throw new DirectoryNotFoundException(nameof(baseDirectory));

            }

            BaseDirectory = baseDirectory;
            PluginConfigurations = new Dictionary<Type, PluginConfiguration>();

            BaseDirectory = baseDirectory;
        }

        public abstract List<PluginConfiguration> ReadConfiguration(bool readFromSource = true);
        public abstract void WriteConfiguration();

        public bool TryAddConfiguration(PluginConfiguration configuration) => PluginConfigurations.TryAdd(configuration.PluginType, configuration);

        public bool RemoveConfiguration(Type type) => PluginConfigurations.Remove(type);
    }
}
