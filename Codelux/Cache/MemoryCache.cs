using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Codelux.Runnables;

namespace Codelux.Cache
{
    public class MemoryCache : RepeatingRunnable, ICache
    {
        private readonly ConcurrentDictionary<string, CachedItem> _cache;
        public int NumberOfItems => _cache.Count;

        public int EvictExpired()
        {
            int evictedCount = 0;

            foreach(KeyValuePair<string, CachedItem> item in _cache)
            {
                CachedItem actual = item.Value;

                if (actual.HasExpired)
                {
                    _cache.Remove(item.Key, out _);
                    evictedCount++;
                }
            }

            return evictedCount;
        }

        public MemoryCache(TimeSpan evictExpiredEvery) : base(evictExpiredEvery)
        {
            _cache = new();
        }

        public void Flush()
        {
            _cache.Clear();
        }

        public bool Remove(string key)
        {
            return _cache.TryRemove(key, out CachedItem _);
        }

        public bool TryGet<T>(string key, out T t)
        {
            t = default;

            if (!_cache.TryGetValue(key, out var item)) return false;
            if (item.HasExpired) return false;

            t = (T) item.Value;

            return true;
        }

        public void Set<T>(string key, T t, ExpirationOptions expirationOptions)
        {
            if (expirationOptions == null) expirationOptions = ExpirationOptions.CreateWithNoExpiration();
            CachedItem item = new(t, expirationOptions);

            _cache.AddOrUpdate(key, item, (k, existingItem) =>
            {
                CachedItem existing = new(t, expirationOptions);
                return existing;
            });
        }

        public bool InsertIfNotExists<T>(string key, T value, ExpirationOptions expirationOptions)
        {
            if (expirationOptions == null) expirationOptions = ExpirationOptions.CreateWithNoExpiration();
            if (TryGet(key, out T _)) return false;

            Set(key, value, expirationOptions);
            return true;
        }

        public override void OnStart(object context = null)
        {
            EvictExpired();
        }

        public override void OnStop() { }
    }
}