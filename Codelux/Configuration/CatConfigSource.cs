using System;
using ConfigCat.Client;

namespace Codelux.Configuration
{
    public class CatConfigSource : ConfigSourceBase
    {
        private readonly IConfigCatClient _client;

        public CatConfigSource(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            _client = ConfigCatClient.Get(apiKey);
        }

        public override bool TryGetString(string key, out string value)
        {
            value = _client.GetValue<string>(key, string.Empty);
            return value != string.Empty;
        }

        public override bool TryGetInt(string key, out int value)
        {
            value = _client.GetValue(key, 0);
            return true;
        }

        public override bool TryGetBool(string key, out bool value)
        {
            value = _client.GetValue(key, false);
            return true;
        }

        public override bool TryGetDouble(string key, out double value)
        {
            value = _client.GetValue(key, 0d);
            return true;
        }

        public override bool TryGetFloat(string key, out float value)
        {
            value = _client.GetValue(key, 0f);
            return true;
        }

        public override bool TryGetDateTime(string key, out DateTime value)
        {
            value = _client.GetValue(key, DateTime.MinValue);
            return value != DateTime.MinValue;
        }
    }
}
