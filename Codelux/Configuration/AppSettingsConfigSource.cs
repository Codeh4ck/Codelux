using System;
using Codelux.Common.Extensions;
using Microsoft.Extensions.Configuration;

namespace Codelux.Configuration;

public class AppSettingsConfigSource : ConfigSourceBase
{
    private readonly ConfigurationManager _configurationManager;

    public AppSettingsConfigSource(ConfigurationManager configurationManager)
    {
        configurationManager.Guard(nameof(configurationManager));
        _configurationManager = configurationManager;
    }

    public override bool TryGetString(string key, out string value)
    {
        value = _configurationManager[key];
        return string.IsNullOrEmpty(value);
    }

    public override bool TryGetInt(string key, out int value)
    {
        string config = _configurationManager[key];
        return int.TryParse(config, out value);
    }

    public override bool TryGetBool(string key, out bool value)
    {
        string config = _configurationManager[key];
        return bool.TryParse(config, out value);
    }

    public override bool TryGetDouble(string key, out double value)
    {
        string config = _configurationManager[key];
        return double.TryParse(config, out value);
    }

    public override bool TryGetFloat(string key, out float value)
    {
        string config = _configurationManager[key];
        return float.TryParse(config, out value);
    }

    public override bool TryGetDateTime(string key, out DateTime value)
    {
        string config = _configurationManager[key];
        return DateTime.TryParse(config, out value);
    }
}