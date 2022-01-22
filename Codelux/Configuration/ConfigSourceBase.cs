using System;
using Codelux.Common.Models;

namespace Codelux.Configuration
{
    public abstract class ConfigSourceBase : IConfigSource
    {
        public abstract bool TryGetString(string key, out string value);
        public abstract bool TryGetInt(string key, out int value);
        public abstract bool TryGetBool(string key, out bool value);
        public abstract bool TryGetDouble(string key, out double value);
        public abstract bool TryGetFloat(string key, out float value);
        public abstract bool TryGetDateTime(string key, out DateTime value);

        public virtual string GetStringOrThrow(string key)
        {
            if (!TryGetString(key, out string value))
                throw new MissingConfigurationException(key);

            return value;
        }

        public virtual int GetIntOrThrow(string key)
        {
            if (!TryGetInt(key, out int value))
                throw new MissingConfigurationException(key);

            return value;
        }

        public virtual bool GetBoolOrThrow(string key)
        {
            if (!TryGetBool(key, out bool value))
                throw new MissingConfigurationException(key);

            return value;
        }

        public virtual double GetDoubleOrThrow(string key)
        {
            if (!TryGetDouble(key, out double value))
                throw new MissingConfigurationException(key);

            return value;
        }

        public virtual float GetFloatOrThrow(string key)
        {
            if (!TryGetFloat(key, out float value))
                throw new MissingConfigurationException(key);

            return value;
        }

        public virtual DateTime GetDateTimeOrThrow(string key)
        {
            if (!TryGetDateTime(key, out DateTime value))
                throw new MissingConfigurationException(key);

            return value;
        }
    }
}
