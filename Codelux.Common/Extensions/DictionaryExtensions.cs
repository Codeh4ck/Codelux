using System.Collections.Generic;

namespace Codelux.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (KeyValuePair<TKey, TValue> pair in values)
                dictionary.Add(pair.Key, pair.Value);
        }
    }
}
