using System;
using System.IO;
using System.Reflection;

namespace Codelux.Common.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetDirectory(this Assembly assembly)
        {
            UriBuilder uri = new(assembly.Location);
            string path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }
    }
}
