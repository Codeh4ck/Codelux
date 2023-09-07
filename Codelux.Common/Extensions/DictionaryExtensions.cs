using System.Collections.Generic;
using System.Collections.Immutable;

namespace Codelux.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (KeyValuePair<TKey, TValue> pair in values)
                dictionary.Add(pair.Key, pair.Value);
        }
        
        public static IDictionary<TKey, TValue> EmptyIfNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => dictionary ?? new Dictionary<TKey, TValue>();
        public static IDictionary<TKey, TValue> EmptyImmutableIfNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => dictionary ?? ImmutableDictionary<TKey, TValue>.Empty;
    }
}