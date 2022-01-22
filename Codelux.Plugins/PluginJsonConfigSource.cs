using Codelux.Plugins.Base;
using Codelux.Plugins.Metadata;
using Newtonsoft.Json;

namespace Codelux.Plugins
{
    public sealed class PluginJsonConfigSource : PluginConfigurationSourceBase
    {
        public PluginJsonConfigSource(string baseDirectory, bool createIfNotExists = true) 
            : base(baseDirectory, createIfNotExists) { }

        public override List<PluginConfiguration> ReadConfiguration(bool readFromSource = true)
        {
            if (!readFromSource && PluginConfigurations.Count > 0)
                return PluginConfigurations.Values.ToList();

            foreach (string file in Directory.GetFiles(BaseDirectory))
            {
                if (!file.EndsWith(".json")) continue;
                string json = File.ReadAllText(file);

                PluginConfiguration config = JsonConvert.DeserializeObject<PluginConfiguration>(json);
                if (config == null) continue;

                TryAddConfiguration(config);
            }

            return PluginConfigurations.Values.ToList();
        }

        public override void WriteConfiguration()
        {
            foreach (var kvp in PluginConfigurations)
            {
                string json = JsonConvert.SerializeObject(kvp.Value);
                string file = Path.Combine(BaseDirectory, $"{kvp.Key.Name}.json");

                File.WriteAllText(file, json);
            }
        }
    }
}
