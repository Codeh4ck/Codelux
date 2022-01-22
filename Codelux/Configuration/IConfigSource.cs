using System;

namespace Codelux.Configuration
{
    public interface IConfigSource
    {
        bool TryGetString(string key, out string value);
        bool TryGetInt(string key, out int value);
        bool TryGetBool(string key, out bool value);
        bool TryGetDouble(string key, out double value);
        bool TryGetFloat(string key, out float value);
        bool TryGetDateTime(string key, out DateTime value);
    }
}
