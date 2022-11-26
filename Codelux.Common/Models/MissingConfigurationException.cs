using System;

namespace Codelux.Common.Models
{
    public class MissingConfigurationException : Exception
    {
        public string ConfigurationName { get; }

        public MissingConfigurationException(string configurationName) => ConfigurationName = configurationName;

        public MissingConfigurationException(string configurationName, string message) : base(message) => ConfigurationName = configurationName;

        public MissingConfigurationException(string configurationName, string message, Exception innerException) 
            : base(message, innerException) => ConfigurationName = configurationName;
    }
}
